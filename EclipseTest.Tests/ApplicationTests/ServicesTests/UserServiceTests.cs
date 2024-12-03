using EclipseTest.Application.Dto.User;
using EclipseTest.Application.Services;
using EclipseTest.Domain.Models;
using EclipseTest.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTest.Tests.ApplicationTests.ServicesTests;

public class UserServiceTests
{
    private UserService _service;

    private Mock<IRepository<User>> _userRepository;

    [SetUp]
    public void Setup()
    {
        _userRepository = new();
        _service = new(_userRepository.Object);
    }

    [Test]
    public void CreateUserAsync_ExistingName_ThrowsArgumentException()
    {
        var existingUser = new User { Name = "John Doe" };

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(existingUser);

        var dto = new CreateUserDto("John Doe", Domain.Enums.UserRole.Manager);

        Assert.ThrowsAsync<ArgumentException>(() => _service.CreateUserAsync(dto));
    }

    [Test]
    public async Task CreateUserAsync_ValidDto_CreatesUser()
    {
        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User)null);

        var dto = new CreateUserDto("John Doe", Domain.Enums.UserRole.Manager);

        await _service.CreateUserAsync(dto);

        _userRepository.Verify(x => x.AddAsync(It.Is<User>(x => x.Name == "John Doe")), Times.Once);
    }
}
