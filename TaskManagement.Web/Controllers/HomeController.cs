using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Data.Enums;
using TaskManagement.Services.Interfaces;
using TaskManagement.Web.Models;
using TaskManagement.Web.Models.Employee;
using TaskManagement.Web.Models.Home;
using TaskManagement.Web.Models.Project;

namespace TaskManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;
        public HomeController(ILogger<HomeController> logger, ITaskService taskService, IEmployeeService employeeService, IProjectService projectService)
        {
            _logger = logger;
            _taskService = taskService;
            _employeeService = employeeService;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            var ss= _taskService.GetAllTasks();
            var tasks = _taskService.GetAllTasks().Select(x => new SimpleTaskViewModel
            {
                Id = x.Id,
                Title = x.Title,
                DueDate= x.DueDate.ToString("dd-MM-yyyy"),
                AssigneeId = x.EmployeeId,
                Status = x.Status == 0 ? "Working on it" : "Done",
                AssigneeName = x.Employee != null ? x.Employee.FullName : null,
                ProjectId= x.ProjectId,
                ProjectName = x.Project.Name
            }).ToList();

            var employees = _employeeService.GetAllEmployees().Select(x => new SimpleEmployeeViewModel
            {
                Id = x.Id,
                Name = x.FullName
            }).ToList();

            var projects = _projectService.GetAllProjects().Select(x => new SimpleProjectViewModel
            {
                Id = x.Id,
                DateOfCreation = x.DateOfCreation.ToString("dd-MM-yyyy"),
                Name = x.Name,
                TasksCount = x.Tasks.Count
            }).ToList();

            var employeesWithMostTasksDoneInPastMonth = _employeeService.EmployeesWithMostCompletedTasks()
                .Select(x => new TopFiveEmployeesViewModel
                {
                    Id = x.Id,
                    CompletedTasks = x.Tasks.Where(t => t.Status == StatusOfTask.Done).Count(),
                    FullName = x.FullName
                }).ToList();

            var projectWithMostDoneTasksInPastMonth = _projectService.TopProjects()
                .Select(x => new TopFiveProjectsViewModel
                {
                    Id = x.Id,
                    CompletedTasks = x.Tasks.Where(t => t.Status == StatusOfTask.Done).Count(),
                    ProjectName = x.Name
                }).ToList();

            var homeViewModel = new HomeViewModel
            {
                EmployeeViewModel = employees,
                TaskViewModel = tasks,
                ProjectViewModel = projects,
                TopFiveEmployeesViewModel = employeesWithMostTasksDoneInPastMonth,
                TopFiveProjectsViewModel = projectWithMostDoneTasksInPastMonth
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}