using System.ComponentModel.DataAnnotations;

namespace EclipseTest.Application.Dto.Project;

public record CreateProjectDto([Required] string Title, [Required] uint UserId);
