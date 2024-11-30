using EclipseTest.Domain.Enums;

namespace EclipseTest.Domain.Models;

public class Todo
{
    private string? _title;
    private string? _description;

    public uint Id { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; private set; }
    public TodoStatus Status { get; private set; }
    public User CreatedBy { get; private set; }
    public List<TodoHistory> History { get; private set; } = new();

    public string Title 
    { 
        get { return _title!; }
        private set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _title = value;
        }
    }

    public string Description
    {
        get { return _description!; }
        private set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _description = value;
        }
    }

    public Todo(
        string title, 
        string description, 
        DateTime dueDate, 
        User createdBy, 
        Priority priority = Priority.Low,
        TodoStatus status = TodoStatus.Pending)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        CreatedBy = createdBy ?? throw new Exception(nameof(createdBy));
        DueDate = dueDate;
        Priority = priority;
        Status = status;
    }

    public void Update(
        string title, 
        string description,
        TodoStatus status,
        DateTime dueDate,
        User updatedBy)
    {
        if (title == null)
            throw new ArgumentNullException(nameof(title));

        if (description == null)
            throw new ArgumentNullException(nameof(description));

        bool hasChanges = false;
        TodoHistory taskHistory = new(updatedBy);

        if (title != Title)
        {
            taskHistory.Changes.Add($"Title field changed from {Title} to {title}");
            hasChanges = true;
        }

        if (description != Description)
        {
            taskHistory.Changes.Add($"Description field changed from {Description} to {description}");
            hasChanges = true;
        }

        if (status != Status)
        {
            taskHistory.Changes.Add($"Status field changed from {Status} to {status}");
            hasChanges = true;
        }

        if (dueDate != DueDate)
        {
            taskHistory.Changes.Add($"Due date field changed from {DueDate} to {dueDate}");
            hasChanges = true;
        }

        if (hasChanges)
        {
            History.Add(taskHistory);
        }
    }
}
