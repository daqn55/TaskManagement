namespace TaskManagement.Web.Models.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public double MonthlySalary { get; set; }
    }
}
