using EclipseTest.Application.Services.Interfaces;
using EclipseTest.Domain.Models;
using EclipseTest.Infrastructure.Interfaces;

namespace EclipseTest.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IRepository<Project> _projectRepository;

    public ProjectService(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
    }

    public async Task<IEnumerable<Project>> GetUserProjectsAsync(int userId)
    {
        return await _projectRepository.FindAllAsync(x => x.CreatedBy.Id == userId);
    }

    public async Task<IEnumerable<Todo>> GetAllProjectTodosAsync(int projectId)
    {
        Project? project = await _projectRepository.FindAsync(x => x.Id == projectId);

        if (project == null)
            throw new ArgumentNullException(nameof(projectId), "Project wasn't found on database");

        return project.Tasks;
    }

    public async Task CreateProjectAsync(Project projectToBeCreated)
    {
        Project? project = await _projectRepository.FindAsync(x => x.Title == projectToBeCreated.Title && x.CreatedBy.Id == x.CreatedBy.Id);

        if (project != null)
            throw new ArgumentException(nameof(projectToBeCreated), "This project is already created!");

        await _projectRepository.AddAsync(projectToBeCreated);
    }
}
