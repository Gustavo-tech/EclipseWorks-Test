using EclipseTest.Domain.Enums;

namespace EclipseTest.Domain.Models;

public class Project : BaseEntity
{
    public string Title { get; set; }
    public User CreatedBy { get; set; }
    public List<Todo> Tasks { get; private set; } = new();

    public bool IsEmpty => Tasks.Count == 0;

    public Project()
    {
    }

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
            throw new ArgumentException("This project achieved the maximum number of tasks");

        Tasks.Add(task);
    }

    public double GenerateAverageForCompletedTodos(int daysToConsider = 30)
    {
        if (IsEmpty)
            throw new ArgumentException("Can't generate report because this project doesn't have any tasks");

        double allCompletedWithinTime = Tasks.Where(x => x.CompletedDate >= DateTime.Now.AddDays(daysToConsider * -1) && x.Status == TodoStatus.Done)
            .Count();

        return allCompletedWithinTime / Tasks.Count;
    }
}
