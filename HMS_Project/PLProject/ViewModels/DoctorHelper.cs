using DALProject.model;
using NuGet.Packaging;
using Project.ViewModels;

namespace PLProject.ViewModels
{
    public static class DoctorHelper
    {
        public static DoctorScheduleLookup MapToEntity(this DoctorScheduleVM viewModel)
        {
            var DoctorSchedule = new DoctorScheduleLookup();
            DoctorSchedule.Id = viewModel.Id;
            DoctorSchedule.Day = viewModel.Day;
            DoctorSchedule.StartTime = TimeOnly.FromTimeSpan(viewModel.StartTime);// Convert TimeSpan to TimeOnly
            DoctorSchedule.EndTime = TimeOnly.FromTimeSpan(viewModel.EndTime); // Convert TimeSpan to TimeOnly
            // Doctor should not be mapped here as it usually comes from the database

            return DoctorSchedule;
        }

        public static DoctorScheduleVM MapToViewModel(this DoctorScheduleLookup entity)
        {
            return new DoctorScheduleVM
            {
                Day = entity.Day,
                StartTime = entity.StartTime.ToTimeSpan(), // Convert TimeOnly to TimeSpan
                EndTime = entity.EndTime.ToTimeSpan(),     // Convert TimeOnly to TimeSpan
                Id= entity.Id,
            };
        }
        public static Doctor UpdatedDoctor(this Doctor doctor,DoctorViewModel doctorViewModel)
        {

            doctor.AppUser.Address= doctorViewModel.Address;
            doctor.AppUser.SSN = doctorViewModel.SSN;
            doctor.AppUser.FullName= $"{doctorViewModel.FirstName.Trim()} {doctorViewModel.MiddleName.Trim()} {doctorViewModel.LastName.Trim()}";
            doctor.AppUser.Gender = doctorViewModel.Gender;
            doctor.AppUser.PhoneNumber = doctorViewModel.Phone;
            doctor.AppUser.Email = doctorViewModel.Email;
            doctor.SpecializationId = doctorViewModel.specializationId ?? 0;

            foreach (var (day, dayVM) in doctor.DoctorScheduleLookups.Zip(doctorViewModel.schedule, (model, ViewModel) => (model, ViewModel)))
            {
                day.Day = dayVM.Day;
                day.StartTime = TimeOnly.FromTimeSpan(dayVM.StartTime);
                day.EndTime = TimeOnly.FromTimeSpan(dayVM.EndTime);
            }
            doctor.DoctorScheduleLookups.AddRange(doctorViewModel.schedule?.Skip(doctor.DoctorScheduleLookups.Count).Select(sv => sv.MapToEntity()).ToList() ?? new List<DoctorScheduleLookup>());
            return doctor;
        }


    }
}
