namespace TaskManagement.Web.Models.Employee
{
    public class EmployeeDetailsViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public ICollection<EmployeeTaskDetailsViewModel> TasksModel { get; set; }
    }
}
