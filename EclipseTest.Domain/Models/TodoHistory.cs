namespace EclipseTest.Domain.Models;

public class TodoHistory : BaseEntity
{
    public uint TaskId { get; set; }
    public List<string> Changes { get; private set; } = new();
    public DateTime Time { get; private set; }
    public User ChangedBy { get; set; }

    public TodoHistory(User changedBy)
    {
        ChangedBy = changedBy;
        Time = DateTime.Now;
    }
}
