using DALProject.model;
using NuGet.Packaging;
using PLProject.ViewModels;
using Project.ViewModels;

public class DoctorViewModel : UserViewModel
{
    public DoctorViewModel() { }

    public DoctorViewModel(Doctor doctor)
    {
        UserId = doctor.AppUser.Id;

        Id = doctor.Id;

        SSN = doctor.AppUser.SSN;

        string[] name = doctor.AppUser.FullName.Split();
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

        DateOfBirth = doctor.AppUser.DateOfBirth;
        Phone = doctor.AppUser.PhoneNumber;
        Email = doctor.AppUser.Email;
        Address = doctor.AppUser.Address;
        Gender = doctor.AppUser.Gender;
        specialization = doctor.DoctorSpecialization?.Specialization ?? "general";
        specializationId = doctor.SpecializationId ?? 0;
        schedule = doctor.DoctorScheduleLookups.Select(schedule => schedule.MapToViewModel()).ToList();
    }

    public string? specialization { get; set; }
    public int? specializationId { get; set; }
    public decimal? Price { get; set; } // إضافة خاصية Price إذا كانت مطلوبة
    public IEnumerable<DoctorSpecializationLookup>? SpecializationsDateReader { get; set; }
    public List<DoctorScheduleVM>? schedule { get; set; } = new List<DoctorScheduleVM>();

    public static explicit operator Doctor(DoctorViewModel doctorViewModel)
    {
        var doctor = new Doctor();
        doctor.AppUser.SSN = doctorViewModel.SSN;
        doctor.AppUser.Email = doctorViewModel.Email;
        doctor.AppUser.Address = doctorViewModel.Address;
        doctor.AppUser.Gender = doctorViewModel.Gender;
        doctor.AppUser.PhoneNumber = doctorViewModel.Phone;
        doctor.AppUser.FullName = $"{doctorViewModel.FirstName.Trim()} {doctorViewModel.MiddleName.Trim()} {doctorViewModel.LastName.Trim()}";
        doctor.AppUser.DateOfBirth = doctorViewModel.DateOfBirth;
        doctor.SpecializationId = doctorViewModel.specializationId ?? 0;
        return doctor;
    }

    public static explicit operator DoctorViewModel(Doctor doctor)
    {
        return new DoctorViewModel(doctor);
    }
}
