using EclipseTest.Application.Dto.User;
using EclipseTest.Domain.Models;

namespace EclipseTest.Application.Services.Interfaces;
public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserDto user);
}