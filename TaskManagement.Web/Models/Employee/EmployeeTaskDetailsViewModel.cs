namespace TaskManagement.Web.Models.Employee
{
    public class EmployeeTaskDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DueDate { get; set; }

        public string? ProjectName { get; set; }
    }
}
