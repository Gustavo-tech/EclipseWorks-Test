using EclipseTest.Domain.Models;

namespace EclipseTest.Application.Services.Interfaces;
public interface IProjectService
{
    Task CreateProjectAsync(Project projectToBeCreated);
    Task<IEnumerable<Todo>> GetAllProjectTodosAsync(int projectId);
    Task<IEnumerable<Project>> GetUserProjectsAsync(int userId);
}