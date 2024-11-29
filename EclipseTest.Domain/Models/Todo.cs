using EclipseTest.Domain.Enums;

namespace EclipseTest.Domain.Models;

public class Todo
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; private set; }
    public User CreatedBy { get; private set; }

    public Todo(string title, string description, DateTime dueDate, User createdBy, Priority priority = Priority.Low)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CreatedBy = createdBy ?? throw new Exception(nameof(createdBy));
        DueDate = dueDate;
        Priority = priority;
    }
}
