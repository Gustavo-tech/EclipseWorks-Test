using EclipseTest.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EclipseTest.Application.Dto.Todo;
public record CreateTodoDto(
    [Required] string Title, 
    [Required] string Description,
    [Required] int ProjectId,
    [Required] int UserId,
    DateTime DueDate, 
    TodoStatus Status,
    Priority Priority);
