using EclipseTest.Application.Dto.Project;
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

    public async Task DeleteProjectAsync(int projectId)
    {
        Project project = await _projectRepository.FindAsync(x => x.Id == projectId);

        if (project is null)
            throw new ArgumentException("Project not found on database");

        if (!project.IsEmpty)
            throw new ArgumentException("Project is not empty, please delete all tasks related to this project first");

        await _projectRepository.DeleteAsync(project);
    }

    public async Task<double> GenerateProjectReportAsync(int projectId)
    {
        Project project = await _projectRepository.FindAsync(x => x.Id == projectId);

        return project is null ? throw new ArgumentException("Project not found on database") : project.GenerateAverageForCompletedTodos(30);
    }
}
