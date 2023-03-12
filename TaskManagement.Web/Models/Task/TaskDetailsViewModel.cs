namespace TaskManagement.Web.Models.Task
{
    public class TaskDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? AssigneeName { get; set; }

        public string Status { get; set; }

        public string ProjectName { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }
    }
}
