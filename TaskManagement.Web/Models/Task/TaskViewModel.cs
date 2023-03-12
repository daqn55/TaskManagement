namespace TaskManagement.Web.Models.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }
    }
}
