namespace TaskManagement.Web.Models.Employee
{
    public class TopFiveEmployeesViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int CompletedTasks { get; set; }
    }
}
