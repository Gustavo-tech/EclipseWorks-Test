using EclipseTest.Application.Dto.User;
using EclipseTest.Application.Services.Interfaces;
using EclipseTest.Domain.Models;
using EclipseTest.Infrastructure.Interfaces;

namespace EclipseTest.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        User userOnDatabase = await _repository.FindAsync(x => x.Name == dto.Name);

        if (userOnDatabase != null)
            throw new ArgumentException("This user already exists!");

        User user = new(dto.Name, dto.Role);
        await _repository.AddAsync(user);
        return await _repository.FindAsync(x => x.Name == dto.Name);
    }
}
