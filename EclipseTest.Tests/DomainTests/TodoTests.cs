using EclipseTest.Domain.Enums;
using EclipseTest.Domain.Models;

namespace EclipseTest.Tests.DomainTests
{
    public class TodoTests
    {
        private Todo _todo;

        [Test]
        public void Update_WhenCalled_SetCorrectProperties()
        {
            User user = new("User");
            DateTime firstDueDate = DateTime.Now.AddDays(10);
            _todo = new("Title", "Description", firstDueDate, user);

            string newTitle = "New Title";
            string newDescription = "New Description";
            TodoStatus newStatus = TodoStatus.Done;
            DateTime newDueDate = DateTime.Now.AddDays(11);

            _todo.Update(newTitle, newDescription, newStatus, newDueDate, user);

            TodoHistory history = _todo.History.First();

            Assert.That(newTitle, Is.EqualTo(_todo.Title));
            Assert.That(newDescription, Is.EqualTo(_todo.Description));
            Assert.That(newStatus, Is.EqualTo(_todo.Status));
            Assert.That(newDueDate, Is.EqualTo(_todo.DueDate));
        }

        [Test]
        public void Update_WhenCalled_CreateTodoHistory()
        {
            User user = new("User");
            DateTime firstDueDate = DateTime.Now.AddDays(10);
            _todo = new("Title", "Description", firstDueDate, user);

            DateTime newDueDate = DateTime.Now.AddDays(11);
            _todo.Update("New Title", "New Description", TodoStatus.Done, newDueDate, user);

            TodoHistory history = _todo.History.First();

            Assert.That(_todo.History.Count(), Is.EqualTo(1));
            Assert.That(history.Changes.Count, Is.EqualTo(4));
            Assert.IsTrue(history.Changes.Any(x => x.Contains("Title") && x.Contains("New Title")));
            Assert.IsTrue(history.Changes.Any(x => x.Contains("Description") && x.Contains("New Description")));
            Assert.IsTrue(history.Changes.Any(x => x.Contains("Pending") && x.Contains("Done")));
            Assert.IsTrue(history.Changes.Any(x => x.Contains(firstDueDate.ToString()) && x.Contains(newDueDate.ToString())));
        }
    }
}
