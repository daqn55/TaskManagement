using TaskManagement.Web.Models.Employee;
using TaskManagement.Web.Models.Project;

namespace TaskManagement.Web.Models.Home
{
    public class HomeViewModel
    {
        public List<SimpleTaskViewModel> TaskViewModel { get; set; }

        public List<SimpleProjectViewModel> ProjectViewModel { get; set; }

        public List<SimpleEmployeeViewModel> EmployeeViewModel { get; set; }

        public List<TopFiveEmployeesViewModel> TopFiveEmployeesViewModel { get;set; }

        public List<TopFiveProjectsViewModel> TopFiveProjectsViewModel { get; set; }
    }
}
