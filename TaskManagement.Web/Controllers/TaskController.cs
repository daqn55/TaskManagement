using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using TaskManagement.Services.Interfaces;
using TaskManagement.Web.Models.Project;
using TaskManagement.Web.Models.Task;
using System.Linq;
using TaskManagement.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;

        public TaskController(ITaskService taskService, IProjectService projectService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _employeeService = employeeService;
        }

        public IActionResult Create()
        {
            var projectsIDs = _projectService.GetAllProjectsIDs();

            return View(projectsIDs);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(TaskViewModel model)
        {
            var task = new Data.Models.Task
            {
                Title = model.Title,
                DueDate = DateTime.Parse(model.DueDate),
                Description = model.Description,
                ProjectId = model.ProjectId,
            };

            _taskService.CreateTask(task);

            return Redirect("../Home/Index");
        }

        public IActionResult Details(int id)
        {
            var task = _taskService.GetTaskById(id);
            var model = new TaskDetailsViewModel
            {
                Id = task.Id,
                DueDate = task.DueDate.ToString("dd-MM-yyyy"),
                Title = task.Title,
                Status = task.Status == 0 ? "Working on it" : "Done",
                ProjectName = task.Project.Name,
                AssigneeName = task.Employee != null ? task.Employee.FullName : null,
                Description = task.Description
            };


            return View(model);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTaskById(id);
            var employees = _employeeService.GetAllEmployees();
            var currentTaskAssignee = employees.FirstOrDefault(x => task.EmployeeId == x.Id);

            var model = new TaskEditViewModel
            {
                Id = task.Id,
                Description = task.Description,
                DueDate = task.DueDate.ToString("yyyy-MM-dd"),
                Title = task.Title,
                ProjectName = task.Project.Name,
                Status = (int)task.Status,
                AssigneeId = task.EmployeeId,
                AssigneeName = currentTaskAssignee != null ? currentTaskAssignee.FullName : null,
                EmployeesModel = employees.Where(x => task.EmployeeId != x.Id).Select(x => new TaskEditEmployeeViewModel
                {
                    FullName = x.FullName,
                    Id = x.Id,
                }).ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(TaskFinishedEditViewModel model)
        {
            var task = _taskService.GetTaskById(model.Id);

            task.Description = model.Description;
            task.DueDate = DateTime.Parse(model.DueDate);
            task.EmployeeId = model.AssigneeId > 0 ? model.AssigneeId : null;
            task.Title = model.Title;
            task.Status = (StatusOfTask)model.StatusOfTask;
            if (task.Status == StatusOfTask.WorkingOnIt)
            {
                task.CompletionDate = DateTime.MinValue;
            }
            else
            {
                task.CompletionDate = DateTime.Today;
            }

            _taskService.UpdateTask(task);

            return Redirect($"../Details/{model.Id}");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _taskService.DeleteTask(id);

            return Redirect("../../Home/Index");
        }

        [Authorize]
        public IActionResult AddAssignee(int taskId, int employeeId)
        {
            var task = _taskService.GetTaskById(taskId);
            task.EmployeeId = employeeId > 0 ? employeeId : null;

            _taskService.UpdateTask(task);

            return Redirect("../../Home/Index");
        }
    }
}
