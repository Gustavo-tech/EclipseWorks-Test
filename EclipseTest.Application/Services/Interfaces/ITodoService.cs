using EclipseTest.Application.Dto.Todo;
using EclipseTest.Domain.Models;

namespace EclipseTest.Application.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetAllProjectTodosAsync(int projectId);
    Task AddTodoToProjectAsync(CreateTodoDto dto);
    Task UpdateTodoFromProjectAsync(UpdateTodoDto dto);
    Task RemoveTodoFromProjectAsync(int id);
}
