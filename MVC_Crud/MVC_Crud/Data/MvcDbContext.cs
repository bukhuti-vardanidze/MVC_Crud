using Microsoft.EntityFrameworkCore;
using MVC_Crud.Models.Domain;

namespace MVC_Crud.Data
{
    public class MvcDbContext : DbContext
    {
        public MvcDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
