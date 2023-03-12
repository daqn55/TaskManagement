namespace TaskManagement.Web.Models.Task
{
    public class TaskFinishedEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DueDate { get; set; }

        public int AssigneeId { get; set; }

        public string Description { get; set; }

        public int StatusOfTask { get; set; }
    }
}
