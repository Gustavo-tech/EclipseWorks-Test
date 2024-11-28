namespace EclipseTest.Infrastructure.Models;

public class User
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }

    public User(string name, string role)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
