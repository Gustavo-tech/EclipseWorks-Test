using EclipseTest.Domain.Enums;
using EclipseTest.Domain.Models;

namespace EclipseTest.Tests.DomainTests;

public class ProjectTests
{
    [Test]
    public void AddTask_MaximumSizeAchieved_ThrowException()
    {
        User user = new("User1");
        Project project = new("MyProject", user);

        Todo task = new("Task", "SomeDescription", DateTime.Now.AddDays(10), user);
        while (project.Tasks.Count != 20)
        {
            project.AddTask(task);
        }

        Assert.That(() => project.AddTask(task), Throws.Exception);
    }

    [Test]
    public void IsEmpty_ContainsTask_ReturnsFalse()
    {
        User user = new("User1");
        Project project = new("MyProject", user);

        Todo task = new("Task", "SomeDescription", DateTime.Now.AddDays(10), user);
        project.AddTask(task);

        Assert.That(project.IsEmpty, Is.EqualTo(false));
    }

    [Test]
    public void IsEmpty_DoesNotContainTask_ReturnsTrue()
    {
        User user = new("User1");
        Project project = new("MyProject", user);

        Assert.That(project.IsEmpty, Is.EqualTo(true));
    }

    [Test]
    public void AddTask_TaskIsNull_ThrowException()
    {
        User user = new("User1");
        Project project = new("MyProject", user);

        Assert.That(() => project.AddTask(null), Throws.Exception);
    }

    [Test]
    public void GenerateAverageForCompletedTodos()
    {
        User user = new("User1");
        Project project = new("MyProject", user);

        while (project.Tasks.Count != 20)
        {
            Todo task = new("Task", "SomeDescription", DateTime.Now.AddDays(10), user);
            project.AddTask(task);
        }

        for (int i = 0; i < 10; i++)
        {
            Todo todo = project.Tasks[i];
            todo.Update(todo.Title, todo.Description, TodoStatus.Done, todo.DueDate, user);
        }

        Assert.That(project.GenerateAverageForCompletedTodos(), Is.EqualTo(0.33).Within(0.1));
    }
}
