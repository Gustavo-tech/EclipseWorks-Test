using EclipseTest.Application.Dto.Project;
using EclipseTest.Application.Dto.Todo;
using EclipseTest.Domain.Models;

namespace EclipseTest.Application.Services.Interfaces;
public interface IProjectService
{
    Task CreateProjectAsync(CreateProjectDto projectToBeCreated);
    Task<IEnumerable<Todo>> GetAllProjectTodosAsync(int projectId);
    Task<IEnumerable<Project>> GetUserProjectsAsync(int userId);
    Task AddTodoToProjectAsync(CreateTodoDto dto);
}