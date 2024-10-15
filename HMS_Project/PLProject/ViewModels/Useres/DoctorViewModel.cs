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
        Email = doctor.Email;
        Address = doctor.Address;
        Gender = doctor.Gender;
        specialization = doctor.DoctorSpecialization.Specialization;
    }

    public string? specialization { get; set; }
    public int? specializationId { get; set; }
    public decimal? Price { get; set; } // إضافة خاصية Price إذا كانت مطلوبة
    public IEnumerable<DoctorSpecializationLookup>? SpecializationsDateReader { get; set; }
    public List<DoctorScheduleLookup> schedule { get; set; } = new List<DoctorScheduleLookup>();

    public static explicit operator Doctor(DoctorViewModel doctorViewModwl)
    {
        return new Doctor()
        {
                
            SSN = doctorViewModwl.SSN,
            Email = doctorViewModwl.Email,
            Address = doctorViewModwl.Address,
            Gender = doctorViewModwl.Gender?? DALProject.model.Gender.Male,
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
