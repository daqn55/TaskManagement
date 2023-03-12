using TaskManagement.Data;
using TaskManagement.Data.Models;
using TaskManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.Enums;

namespace TaskManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public void DeleteEmployee(int employeeId)
        {
            var employee = _dbContext.Employees.Include(x => x.Tasks).First(x => x.Id == employeeId);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
        }

        public void EditEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
        }

        public ICollection<Employee> EmployeesWithMostCompletedTasks()
        {
            var employees = _dbContext.Employees
                .Where(x => x.Tasks.Where(t => t.Status == StatusOfTask.Done).Count() > 0)
                .ToList();
            employees = employees
                .OrderByDescending(x => x.Tasks.Where(t => t.Status == StatusOfTask.Done && t.CompletionDate.Month == DateTime.Today.Month).Count())
                .Take(5)
                .ToList();

            return employees;
        }

        public ICollection<Data.Models.Task> EmployeeTasks(int empoyeeId)
        {
            var tasks = _dbContext.Employees.Include(x => x.Tasks).First(x => x.Id == empoyeeId).Tasks.ToList();

            return tasks;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            var employee = _dbContext.Employees.First(x => x.Id == id);

            return employee;
        }


    }
}