using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DALProject.model;

public class HmsUser
{
    public long SSN { get; set; } 
    public string FullName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; } 
    public string? Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
    public string? Address { get; set; }
    public string? Gender { get; set; }
}
