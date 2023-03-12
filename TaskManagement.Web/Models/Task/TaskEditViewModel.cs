namespace TaskManagement.Web.Models.Task
{
    public class TaskEditViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public string ProjectName { get; set; }

        public int Status { get; set; }

        public int? AssigneeId { get; set; }

        public string? AssigneeName { get; set; }

        public ICollection<TaskEditEmployeeViewModel> EmployeesModel { get; set; }
    }
}
