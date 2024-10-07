using DALProject.model;

public class DoctorViewModel
{
    public DoctorViewModel() { }

    public DoctorViewModel(Doctor doctor)
    {
        Id = doctor.Id;
        SSN = doctor.SSN;
        string[] name = doctor.FullName.Split();
        if (name.Length == 3)
        {
            FirstName = name[0];
            MiddleName = name[1];
            LastName = name[2];
        }
        else if (name.Length == 2)
        {
            FirstName = name[0];
            LastName = name[1];
        }
        DateOfBirth = doctor.DateOfBirth;
        Phone = doctor.Phone;
        Email = doctor.Email;
        UserPassword = doctor.UserPassword;
        Address = doctor.Address;
        Gender = Enum.TryParse(doctor.Gender, out Gender Gendervalue) ? Gendervalue : null;
        specialization = doctor.DoctorSpecialization.Specialization;
    }

    public int Id { get; set; }  
    public long SSN { get; set; }
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? Phone { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
    public string? Address { get; set; }
    public Gender? Gender { get; set; }
    public string? specialization { get; set; }
    public int? specializationId { get; set; }
    public decimal? Price { get; set; } // إضافة خاصية Price إذا كانت مطلوبة
    public IEnumerable<DoctorSpecializationLookup>? SpecializationsDateReader { get; set; }

    public static explicit operator Doctor(DoctorViewModel doctorViewModwl)
    {
        return new Doctor()
        {
                
            SSN = doctorViewModwl.SSN,
            Email = doctorViewModwl.Email,
            UserPassword = doctorViewModwl.UserPassword,
            Address = doctorViewModwl.Address,
            Gender = doctorViewModwl.Gender.ToString(),
            Phone = doctorViewModwl.Phone,
            FullName = $"{doctorViewModwl.FirstName.Trim()} {doctorViewModwl.MiddleName.Trim()} {doctorViewModwl.LastName.Trim()}",
            DateOfBirth = doctorViewModwl.DateOfBirth,
            SpecializationId = doctorViewModwl.specializationId??0,
        };
    }

    public static explicit operator DoctorViewModel(Doctor doctor)
    {
        return new DoctorViewModel(doctor);
    }
}
