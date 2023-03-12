using TaskManagement.Data.Models;

namespace TaskManagement.Services.Interfaces
{
    public interface IProjectService
    {
        void CreateProject(Project project);
        ICollection<KeyValuePair<string, int>> GetAllProjectsIDs();

        ICollection<Project> GetAllProjects();

        Project GetProject(int id);

        void UpdateProject(Project project);

        ICollection<Project> TopProjects();
    }
}
