using EclipseTest.Application.Dto.Project;
using EclipseTest.Application.Dto.Todo;
using EclipseTest.Application.Services.Interfaces;
using EclipseTest.Domain.Models;
using EclipseTest.Infrastructure.Interfaces;

namespace EclipseTest.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IRepository<User> _userRepository;

    public ProjectService(IRepository<Project> projectRepository, IRepository<User> userRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<IEnumerable<Project>> GetUserProjectsAsync(int userId)
    {
        return await _projectRepository.FindAllAsync(x => x.CreatedBy.Id == userId);
    }

    public async Task<IEnumerable<Todo>> GetAllProjectTodosAsync(int projectId)
    {
        Project? project = await _projectRepository.FindAsync(x => x.Id == projectId);

        if (project == null)
            throw new ArgumentNullException("Project wasn't found on database");

        return project.Tasks;
    }

    public async Task CreateProjectAsync(CreateProjectDto projectToBeCreated)
    {
        Project? project = await _projectRepository.FindAsync(x => x.Title == projectToBeCreated.Title && x.CreatedBy.Id == x.CreatedBy.Id);

        if (project != null)
            throw new ArgumentException("This project is already created!");

        User user = await _userRepository.FindAsync(x => x.Id == projectToBeCreated.UserId);
        if (user == null)
            throw new ArgumentException("User not found!");

        project = new(projectToBeCreated.Title, user);
        await _projectRepository.AddAsync(project);
    }

    public async Task AddTodoToProjectAsync(CreateTodoDto dto)
    {
        Project project = await _projectRepository.FindAsync(x => x.CreatedBy.Id == dto.UserId && x.Id == dto.ProjectId);
        User user = await _userRepository.FindAsync(x => x.Id == dto.UserId);

        Todo newTodo = new(dto.Title, dto.Description, dto.DueDate, user, dto.Priority, dto.Status);
        project.AddTask(newTodo);

        await _projectRepository.UpdateAsync(project);
    }
}
