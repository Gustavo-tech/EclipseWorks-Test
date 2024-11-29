namespace EclipseTest.Domain.Models;

public class Project
{
    public uint Id { get; set; }
    public string Title { get; set; }
    public User CreatedBy { get; set; }
    public List<Todo> Tasks { get; private set; } = new();

    public bool IsEmpty => Tasks.Count == 0;

    public Project(string title, User createdBy)
    {
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public void AddTask(Todo task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (Tasks.Count >= 20)
            throw new Exception("This project achieved the maximum number of tasks");

        Tasks.Add(task);
    }
}
