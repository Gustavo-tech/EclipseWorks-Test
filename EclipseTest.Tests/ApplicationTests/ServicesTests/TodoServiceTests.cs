using EclipseTest.Application.Dto.Todo;
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

public class TodoServiceTests
{
    private TodoService _service;

    private Mock<IRepository<User>> _userRepository;
    private Mock<IRepository<Project>> _projectRepository;
    private Mock<IRepository<Todo>> _todoRepository;

    [SetUp]
    public void Setup()
    {
        _userRepository = new();
        _projectRepository = new();
        _todoRepository = new();

        _service = new(_projectRepository.Object, _userRepository.Object, _todoRepository.Object);
    }

    [Test]
    public async Task GetAllProjectTodosAsync_WhenProjectExists_ShouldReturnProjectTodos()
    {
        var project = new Project { Id = 1, Tasks = new List<Todo>() };
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                          .ReturnsAsync(project);

        var todos = await _service.GetAllProjectTodosAsync(1);

        Assert.That(todos, Is.EquivalentTo(project.Tasks));
    }

    [Test]
    public void GetAllProjectTodosAsync_WhenProjectNotFound_ShouldThrowException()
    {
        int projectId = 1;
        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                          .ReturnsAsync((Project)null);

        Assert.That(async () => await _service.GetAllProjectTodosAsync(projectId), Throws.Exception);
    }

    [Test]
    public async Task AddTodoToProjectAsync_WhenProjectAndUserExist_ShouldAddTodo()
    {
        var project = new Project();
        var user = new User();
        var createDto = new CreateTodoDto("Title", "Description", 1, 1, DateTime.Now.AddDays(10), 
            Domain.Enums.TodoStatus.Pending, Domain.Enums.Priority.Medium);

        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                          .ReturnsAsync(project);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                          .ReturnsAsync(user);

        await _service.AddTodoToProjectAsync(createDto);

        _projectRepository.Verify(x => x.UpdateAsync(It.IsAny<Project>()), Times.Once());
    }

    [Test]
    public void AddTodoToProjectAsync_WhenProjectNotFound_ShouldThrowException()
    {
        var user = new User();
        var createDto = new CreateTodoDto("Title", "Description", 1, 1,
            DateTime.Now.AddDays(10), Domain.Enums.TodoStatus.Pending, Domain.Enums.Priority.Medium);

        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                          .ReturnsAsync((Project)null);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                          .ReturnsAsync(user);

        Assert.That(() => _service.AddTodoToProjectAsync(createDto), Throws.Exception);
    }

    [Test]
    public void AddTodoToProjectAsync_WhenUserNotFound_ShouldThrowException()
    {
        var user = new User();
        var createDto = new CreateTodoDto("Title", "Description", 1, 1,
            DateTime.Now.AddDays(10), Domain.Enums.TodoStatus.Pending, Domain.Enums.Priority.Medium);

        _projectRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Project, bool>>>()))
                          .ReturnsAsync(new Project());

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                          .ReturnsAsync((User) null);

        Assert.That(() => _service.AddTodoToProjectAsync(createDto), Throws.Exception);
    }

    [Test]
    public async Task RemoveTodoFromProjectAsync_WhenTodoExists_ShouldRemoveTodo()
    {
        int todoId = 1;
        var todo = new Todo();
        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .ReturnsAsync(todo);

        await _service.RemoveTodoFromProjectAsync(todoId);

        _todoRepository.Verify(x => x.DeleteAsync(todo), Times.Once());
    }

    [Test]
    public void RemoveTodoFromProjectAsync_WhenTodoNotFound_ShouldThrowArgumentException()
    {
        int todoId = 1;
        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .ReturnsAsync((Todo) null);

        Assert.ThrowsAsync<ArgumentException>(() => _service.RemoveTodoFromProjectAsync(todoId));
    }

    [Test]
    public async Task UpdateTodoFromProjectAsync_ShouldUpdateTodo_WhenTodoAndUserExist()
    {
        int todoId = 1;
        int userId = 2;
        var todo = new Todo();
        var user = new User();
        var updateDto = new UpdateTodoDto(todoId, userId, "New Title", "New Description", Domain.Enums.TodoStatus.Pending, DateTime.Now.AddDays(10));

        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .ReturnsAsync(todo);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                      .ReturnsAsync(user);

        await _service.UpdateTodoFromProjectAsync(updateDto);

        _todoRepository.Verify(x => x.UpdateAsync(todo), Times.Once);
    }

    [Test]
    public void UpdateTodoFromProjectAsync_WhenTodoNotFound_ShouldThrowArgumentException()
    {
        int todoId = 1;
        int userId = 2;
        var updateDto = new UpdateTodoDto(todoId, userId, "New Title", "New Description", Domain.Enums.TodoStatus.Pending, DateTime.Now.AddDays(10));

        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .ReturnsAsync((Todo) null);

        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                      .ReturnsAsync(new User());

        Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateTodoFromProjectAsync(updateDto));
    }

    [Test]
    public void UpdateTodoFromProjectAsync_WhenUserNotFound_ShouldThrowArgumentException()
    {
        int todoId = 1;
        int userId = 2;
        var todo = new Todo();
        var updateDto = new UpdateTodoDto(todoId, userId, "New Title", "New Description", Domain.Enums.TodoStatus.Pending, DateTime.Now.AddDays(10));

        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .Returns(Task.FromResult(todo));
        _userRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
                      .Returns(Task.FromResult<User>(null));

        Assert.ThrowsAsync<ArgumentException>(() =>_service.UpdateTodoFromProjectAsync(updateDto));
    }

    [Test]
    public async Task AddCommentToTodoAsync_WhenTodoExists_ShouldAddComment()
    {
        int todoId = 1;
        var todo = new Todo();
        var createCommentDto = new CreateCommentDto(1, "Comment");

        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .Returns(Task.FromResult(todo));

        await _service.AddCommentToTodoAsync(createCommentDto);

        _todoRepository.Verify(x => x.UpdateAsync(todo), Times.Once);
    }

    [Test]
    public void AddCommentToTodoAsync_WhenTodoNotFound_ShouldThrowArgumentException()
    {
        int todoId = 1;
        var createCommentDto = new CreateCommentDto(1, "Comment");

        _todoRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Todo, bool>>>()))
                      .Returns(Task.FromResult<Todo>(null));

        Assert.ThrowsAsync<ArgumentException>(() => _service.AddCommentToTodoAsync(createCommentDto));
    }
}
