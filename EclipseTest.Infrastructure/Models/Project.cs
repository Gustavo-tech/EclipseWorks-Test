
namespace EclipseTest.Infrastructure.Models;
public class Project
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public List<Task> Tasks { get; private set; } = new();

    public Project(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }
}
