using TaskManagement.Web.Models.Task;

namespace TaskManagement.Web.Models.Project
{
    public class ProjectDetailsViewModel
    {
        public string Name { get; set; }

        public string DateOfCreation { get; set; }

        public string Description { get; set; }

        public ICollection<ProjectTaskViewModel> TasksModel { get; set; }
    }
}
