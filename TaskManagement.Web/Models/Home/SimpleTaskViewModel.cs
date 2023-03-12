namespace TaskManagement.Web.Models.Home
{
    public class SimpleTaskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DueDate { get; set; }

        public int? AssigneeId { get; set; }

        public string Status { get; set; }

        public string? AssigneeName { get; set; }

        public int? ProjectId { get; set; }

        public string? ProjectName { get; set; }
    }
}
