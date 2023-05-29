using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Crud.Data;
using MVC_Crud.Models;
using MVC_Crud.Models.Domain;

namespace MVC_Crud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MvcDbContext _context;

        public EmployeesController(MvcDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBith = addEmployeeRequest.DateOfBith
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                var viewModel = new UpdateEmployeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBith = employee.DateOfBith
                };

                return await Task.Run(() => View("View",viewModel));

            }
      
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeViewModel viewModel)
        {
            var employee = await _context.Employees.FindAsync(viewModel.Id);
          
            if(employee != null)
            {
                employee.Name= viewModel.Name;
                employee.Email= viewModel.Email;
                employee.Salary= viewModel.Salary;
                employee.Department= viewModel.Department;
                employee.DateOfBith= viewModel.DateOfBith;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeViewModel viewModel)
        {
            var employee = await _context.Employees.FindAsync(viewModel.Id);
            if(employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
