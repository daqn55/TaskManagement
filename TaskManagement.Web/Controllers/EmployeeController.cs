using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data.Models;
using TaskManagement.Services.Interfaces;
using TaskManagement.Web.Models.Employee;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;

        public EmployeeController(IEmployeeService employeeService, ITaskService taskService)
        {
            _employeeService = employeeService;
            _taskService = taskService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            var employee = new Employee
            {
                FullName = model.FullName,
                Email = model.Email,
                DateOfBirth = DateTime.Parse(model.DateOfBirth),
                MonthlySalary = (decimal)model.MonthlySalary,
                PhoneNumber = model.PhoneNumber,
            };

            _employeeService.AddEmployee(employee);

            return Redirect("../Home/Index");
        }

        public IActionResult Details()
        {
            var employees = _employeeService.GetAllEmployees()
                .Select(x => new EmployeeDetailsViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    DateOfBirth = x.DateOfBirth.ToString("dd-MM-yyyy"),
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Salary = x.MonthlySalary,
                    TasksModel = x.Tasks.Select(t => new EmployeeTaskDetailsViewModel
                    {
                        Id = t.Id,
                        DueDate = t.DueDate.ToString("dd-MM-yyyy"),
                        ProjectName = t.Project != null ? t.Project.Name : null,
                        Title = t.Title
                    }).ToList()
                }).ToList();

            return View(employees);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                DateOfBirth = employee.DateOfBirth.ToString("yyyy-MM-dd"),
                MonthlySalary = (double)employee.MonthlySalary,
                Email = employee.Email,
                FullName = employee.FullName,
                PhoneNumber = employee.PhoneNumber
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            var employee = _employeeService.GetEmployee(model.Id);

            employee.PhoneNumber = model.PhoneNumber;
            employee.FullName = model.FullName;
            employee.Email = model.Email;
            employee.MonthlySalary = (decimal)model.MonthlySalary;
            employee.DateOfBirth = DateTime.Parse(model.DateOfBirth);

            _employeeService.EditEmployee(employee);

            return Redirect("../Details");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);

            return Redirect("../Details");
        }

        [Authorize]
        public IActionResult GetTasksOfEmployee(int id)
        {
            var employeeTasks = _employeeService.EmployeeTasks(id)
                .Select(x => new EmployeeTasksViewModel
                {
                    Id = x.Id,
                    DueDate = x.DueDate.ToString("dd-MM-yyyy"),
                    Title = x.Title
                }).ToList();

            var htmlRaw = string.Empty;
            int count = 1;
            foreach (var task in employeeTasks)
            {
                htmlRaw += $"<tr>\r\n<th scope=\"row\">{count}</th>\r\n<td>{task.Title}</td>\r\n<td>{task.DueDate}</td>\r\n</tr>";
                count++;
            }

            if (string.IsNullOrEmpty(htmlRaw))
            {
                htmlRaw = "No data";
            }

            return Content(htmlRaw);
        }
    }
}
