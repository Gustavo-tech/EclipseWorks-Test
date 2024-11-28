using EclipseTest.Infrastructure.Enums;

namespace EclipseTest.Infrastructure.Models;

public class Task
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public User? CreatedBy { get; set; }
}
