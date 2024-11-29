using EclipseTest.Domain.Enums;

namespace EclipseTest.Domain.Models;

public class User
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }

    public User(string name, UserRole role = UserRole.Normal)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
