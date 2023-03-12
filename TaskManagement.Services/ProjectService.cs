using TaskManagement.Data;
using TaskManagement.Data.Models;
using TaskManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProjectService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateProject(Project project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }

        public ICollection<Project> GetAllProjects()
        {
            return _dbContext.Projects.Include(x => x.Tasks).ToList();
        }

        public ICollection<KeyValuePair<string, int>> GetAllProjectsIDs()
        {
            var projectIDs = _dbContext.Projects.Select(x => new KeyValuePair<string, int>(x.Name, x.Id)).ToList();

            return projectIDs;
        }

        public Project GetProject(int id)
        {
            var project = _dbContext.Projects.First(x => x.Id == id);

            return project;
        }

        public ICollection<Project> TopProjects()
        {
            var projects = _dbContext.Projects
                .Where(x => x.Tasks.Where(t => t.Status == Data.Enums.StatusOfTask.Done).Count() > 0)
                .ToList();

            projects = projects.OrderByDescending(x => x.Tasks.Where(t => t.CompletionDate.Month == DateTime.Today.Month).Count())
                .Take(5)
                .ToList();

            return projects;
        }

        public void UpdateProject(Project project)
        {
            _dbContext.Projects.Update(project);
            _dbContext.SaveChanges();
        }
    }
}
