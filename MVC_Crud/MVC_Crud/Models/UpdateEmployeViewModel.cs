namespace MVC_Crud.Models
{
	public class UpdateEmployeViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public double Salary { get; set; }
		public DateTime DateOfBith { get; set; }
		public string Department { get; set; }
	}
}
