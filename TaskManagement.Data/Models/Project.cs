namespace TaskManagement.Data.Models
{
    public class Project
    {
        public Project()
        {
            Tasks= new List<Task>();
            Employees= new List<Employee>();
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string? Description { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
