using EclipseTest.Application.Dto.Project;
using EclipseTest.Application.Services;
using EclipseTest.Domain.Enums;
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

public class ProjectServiceTests
{
    private ProjectService _service;

    private Mock<IRepository<Project>> _projectRepository;
    private Mock<IRepository<User>> _userRepository;

    [SetUp]
    public void Setup()
    {
        _projectRepository = new();
        _userRepository = new();

        _service = new(_projectRepository.Object, _userRepository.Object);
    }

    [Test]
    public async Task GetUserProjectsAsync_WhenCalled_ReturnsUserProjects()
    {
        Project project = new();

        _projectRepository.Setup(x => x.FindAllAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync([project]);

        IEnumerable<Project> output = await _service.GetUserProjectsAsync(1);

        Assert.That(output.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CreateProjectAsync_ProjectAlreadyExists_ThrowException()
    {
        Project project = new();

        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(project);

        Assert.That(() => _service.CreateProjectAsync(new CreateProjectDto("Title", 1)), Throws.Exception);
    }

    [Test]
    public void CreateProjectAsync_UserDoesntExists_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync((Project)null);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User) null);

        Assert.That(() => _service.CreateProjectAsync(new CreateProjectDto("Title", 1)), Throws.Exception);
    }

    [Test]
    public async Task CreateProjectAsync_WhenCalled_CreateProject()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync((Project)null);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User("User", UserRole.Manager));

        CreateProjectDto dto = new("Title", 1);
        await _service.CreateProjectAsync(dto);

        _projectRepository.Verify(x => x.AddAsync(It.Is<Project>(x => x.Title == dto.Title)), Times.Once());
    }

    [Test]
    public async Task DeleteProject_WhenCalled_DeletesTheProject()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(new Project() { Id = 1});

        await _service.DeleteProjectAsync(1);

        _projectRepository.Verify(x => x.DeleteAsync(It.Is<Project>(x => x.Id == 1)), Times.Once());
    }

    [Test]
    public void DeleteProject_ProjectDoesntExists_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync((Project)null);

        Assert.That(() => _service.DeleteProjectAsync(1), Throws.Exception);
    }

    [Test]
    public void DeleteProject_ProjectIsNotEmpty_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(new Project() { Tasks = [new Todo()] });

        Assert.That(() => _service.DeleteProjectAsync(1), Throws.Exception);
    }

    [Test]
    public void GenerateReport_ProjectDoesntExists_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync((Project)null);

        Assert.That(() => _service.GenerateProjectReportAsync(1, 1), Throws.Exception);
    }

    [Test]
    public void GenerateReport_UserDoesntExists_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(new Project());

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync((User)null);

        Assert.That(() => _service.GenerateProjectReportAsync(1, 1), Throws.Exception);
    }

    [Test]
    public void GenerateReport_UserIsNotManager_ThrowException()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(new Project());

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User("User", UserRole.Normal));

        Assert.That(() => _service.GenerateProjectReportAsync(1, 1), Throws.Exception);
    }

    [Test]
    public void GenerateReport_WhenCalled_ReturnsAverageForCompletedTodos()
    {
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
            .ReturnsAsync(new Project() { Tasks = [new Todo()] });

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User("User", UserRole.Normal));

        Assert.That(() => _service.GenerateProjectReportAsync(1, 1), Is.EqualTo(0));
    }
}
