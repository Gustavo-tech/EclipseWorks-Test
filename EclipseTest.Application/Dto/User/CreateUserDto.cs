using EclipseTest.Domain.Enums;

namespace EclipseTest.Application.Dto.User;


public record CreateUserDto(string Name, UserRole Role);
