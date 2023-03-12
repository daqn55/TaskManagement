namespace TaskManagement.Web.Models.Project
{
    public class ProjectTaskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public string? AssigneeName { get; set; }

        public string DueDate { get; set; }
    }
}
