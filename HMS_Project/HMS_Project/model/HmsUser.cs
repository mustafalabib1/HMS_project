using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS_Project.model;

public partial class HmsUser
{
    public long SSN { get; set; }
    public virtual FullName FullName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? Phone { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
    public string? Address { get; set; }
    public Gender? Gender { get; set; }
}
