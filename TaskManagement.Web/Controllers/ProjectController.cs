using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.Models;
using TaskManagement.Services.Interfaces;
using TaskManagement.Web.Models.Project;
using TaskManagement.Web.Models.Task;

namespace TaskManagement.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public ProjectController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProjectViewModel model)
        {
            var project = new Project
            {
                Name = model.Name,
                Description = model.Description,
                DateOfCreation = DateTime.Today
            };

            _projectService.CreateProject(project);

            return Redirect("../Home/Index");
        }

        public IActionResult Details(int id)
        {
            var project = _projectService.GetProject(id);

            var model = new ProjectDetailsViewModel
            {
                Name = project.Name,
                DateOfCreation = project.DateOfCreation.ToString("dd-MM-yyyy"),
                Description = project.Description,
                TasksModel = _taskService.GetTasksByProjectId(id)
                        .Select(x => new ProjectTaskViewModel
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Status = x.Status == Data.Enums.StatusOfTask.WorkingOnIt ? "Working on it" : "Done",
                            DueDate = x.DueDate.ToString("yy-MM-yyyy"),
                            AssigneeName = x.Employee != null ? x.Employee.FullName : null
                        }).ToList()
            };

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var project = _projectService.GetProject(id);

            var model = new ProjectViewModel
            {
                Id = id,
                Name = project.Name,
                Description = project.Description
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProjectViewModel model)
        {
            var project = _projectService.GetProject(model.Id);

            project.Name = model.Name;
            project.Description = model.Description;

            _projectService.UpdateProject(project);

            return Redirect($"../Details/{model.Id}");
        }
    }
}
