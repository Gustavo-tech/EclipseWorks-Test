using EclipseTest.Domain.Enums;

namespace EclipseTest.Api.Dto.User;

public record CreateUserDto(string Name, UserRole Role);
