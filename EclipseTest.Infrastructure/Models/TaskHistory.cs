namespace EclipseTest.Infrastructure.Models;

public class TaskHistory
{
    public uint Id { get; set; }
    public uint TaskId { get; set; }
    public List<string> Changes { get; private set; } = new();
    public DateTime Time { get; private set; }

    public TaskHistory()
    {
        Time = DateTime.Now;
    }
}
