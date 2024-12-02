using System.ComponentModel.DataAnnotations;

namespace EclipseTest.Application.Dto.Todo;

public record CreateCommentDto([Required] int Id, [Required] string Comment);
