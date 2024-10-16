using DALProject.model;
using NuGet.Packaging;
using PLProject.ViewModels;
using Project.ViewModels;

public class DoctorViewModel : UserViewModel
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
        Phone = doctor.PhoneNumber;
        Email = doctor.Email;
        Address = doctor.Address;
        Gender = doctor.Gender;
        specialization = doctor.DoctorSpecialization.Specialization;
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
        doctor.SSN = doctorViewModel.SSN;
        doctor.Email = doctorViewModel.Email;
        doctor.Address = doctorViewModel.Address;
        doctor.Gender = doctorViewModel.Gender;
        doctor.PhoneNumber = doctorViewModel.Phone;
        doctor.FullName = $"{doctorViewModel.FirstName.Trim()} {doctorViewModel.MiddleName.Trim()} {doctorViewModel.LastName.Trim()}";
        doctor.DateOfBirth = doctorViewModel.DateOfBirth;
        doctor.SpecializationId = doctorViewModel.specializationId ?? 0;
        return doctor;
    }

    public static explicit operator DoctorViewModel(Doctor doctor)
    {
        return new DoctorViewModel(doctor);
    }
}
