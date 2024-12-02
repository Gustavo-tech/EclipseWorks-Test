using EclipseTest.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EclipseTest.Application.Dto.Todo;

public record UpdateTodoDto(
    [Required] int Id,
    [Required] int UserId,
    [Required] string Title,
    [Required] string Description,
    [Required] TodoStatus Status,
    [Required] DateTime DueDate);
