using DALProject.model;
using PLProject.ViewModels;

public class DoctorViewModel:UserViewModel
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
