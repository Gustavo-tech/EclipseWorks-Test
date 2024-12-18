﻿using EclipseTest.Domain.Enums;

namespace EclipseTest.Domain.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public UserRole Role { get; set; }

    public User()
    {
    }

    public User(string name, UserRole role = UserRole.Normal)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
