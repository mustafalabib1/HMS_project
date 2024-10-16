using DALProject.model;
using System.ComponentModel.DataAnnotations;

namespace PLProject.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "SSN is required.")]
        public long SSN { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Middle Name is required.")]
        [StringLength(50, ErrorMessage = "Middle Name can't be longer than 50 characters.")]
        public string MiddleName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters.")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateOnly DateOfBirth { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number can't be longer than 15 characters.")]
        public string? Phone { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "Email address can't be longer than 100 characters.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; } = null!;

        [StringLength(250, ErrorMessage = "Address can't be longer than 250 characters.")]
        public string? Address { get; set; }

        public string? Gender { get; set; }
    }

}
