using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS_Project.model;

public partial class HmsUser
{
    public long SSN { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
    public string? Address { get; set; }
    public char? Gender { get; set; }
}
