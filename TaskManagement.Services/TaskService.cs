using TaskManagement.Data;
using TaskManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = TaskManagement.Data.Models.Task;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateTask(Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
            var task = _dbContext.Tasks.First(x => x.Id == taskId);
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
        }

        public ICollection<Task> GetAllTasks()
        {
            var tasks = _dbContext.Tasks.Include(x => x.Employee).Include(x => x.Project).ToList();

            return tasks;
        }

        public Task GetTaskById(int taskId)
        {
            var task = _dbContext.Tasks.Include(x => x.Project).Include(x => x.Employee).First(x => x.Id == taskId);

            return task;
        }

        public ICollection<Task> GetTasksByEmployee(int employeeId)
        {
            var employeeTasks = _dbContext.Tasks.Where(x => x.EmployeeId == employeeId).ToList();

            return employeeTasks;
        }

        public ICollection<Task> GetTasksByProjectId(int projectId)
        {
            var tasks = _dbContext.Tasks.Include(x => x.Employee).Where(x => x.ProjectId == projectId).ToList();

            return tasks;
        }

        public void UpdateTask(Task task)
        {
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
        }
    }
}
