namespace TaskManagement.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal MonthlySalary { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
