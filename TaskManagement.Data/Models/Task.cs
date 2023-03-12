using TaskManagement.Data.Enums;

namespace TaskManagement.Data.Models
{
    public class Task
    {
        public Task()
        {
            Status = StatusOfTask.WorkingOnIt;
        }

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime DueDate { get; set; }

        public StatusOfTask Status { get; set; }

        public DateTime CompletionDate { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
