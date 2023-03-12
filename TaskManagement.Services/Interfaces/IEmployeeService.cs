using TaskManagement.Data.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);

        ICollection<Employee> GetAllEmployees();

        void EditEmployee(Employee employee);

        Employee GetEmployee(int employeeId);

        void DeleteEmployee(int employeeId);

        ICollection<Data.Models.Task> EmployeeTasks(int empoyeeId);

        ICollection<Employee> EmployeesWithMostCompletedTasks();
    }
}
