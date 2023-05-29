using Microsoft.AspNetCore.Mvc;
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
                DateOfBith = addEmployeeRequest.DateOfBith,
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Add");
        }
    }
}
