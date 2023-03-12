using TaskManagement.Data.Models;
using Task = TaskManagement.Data.Models.Task;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        void CreateTask(Task task);

        ICollection<Task> GetAllTasks();

        ICollection<Task> GetTasksByProjectId(int projectId);

        Task GetTaskById(int taskId);

        ICollection<Task> GetTasksByEmployee(int employeeId);

        void UpdateTask(Task task);

        void DeleteTask(int taskId);
    }
}
