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

    public async Task<User> CreateUserAsync(User user)
    {
        User userOnDatabase = await _repository.FindAsync(x => x.Name == user.Name);

        if (userOnDatabase != null)
            throw new ArgumentException("This user already exists!");

        await _repository.AddAsync(user);
        return await _repository.FindAsync(x => x.Name == user.Name);
    }
}
