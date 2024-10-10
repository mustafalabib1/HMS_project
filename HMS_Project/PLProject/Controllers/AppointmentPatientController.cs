using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PLProject.ViewModels.AppointmentViewModel;
using X.PagedList;

namespace PLProject.Controllers
{
    public class AppointmentPatientController : Controller
    {
        private readonly IRepository<Apointment> appointmentRepo;
        private readonly IRepository<Clinic> clinicRepo;
        private readonly IRepository<Receptionist> receptionistRepo;
        private readonly IRepository<Doctor> doctorRepo;
        private readonly IRepository<DoctorScheduleLookup> doctorScheduleRepo;

        public AppointmentPatientController(IRepository<Apointment> appointmentRepo,
            IRepository<Clinic> clinicRepo,
            IRepository<Receptionist> receptionistRepo,
            IRepository<Doctor> doctorRepo,
            IRepository<DoctorScheduleLookup> doctorScheduleRepo)
        {
            this.appointmentRepo = appointmentRepo;
            this.clinicRepo = clinicRepo;
            this.receptionistRepo = receptionistRepo;
            this.doctorRepo = doctorRepo;
            this.doctorScheduleRepo = doctorScheduleRepo;
        }

		#region Get all Appointment for patient 
		public IActionResult Index( int? page,int PatientId= 1)
		{

			var appointments = appointmentRepo.Find(a => a.PatientId == PatientId).Include(a=>a.Patient).Include(a=>a.Doctor).Include(a=>a.Clinic).ToList();
            var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
			// Pagination logic
			int pageSize = 10;
			int pageNumber = page ?? 1;

			var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

			return View(paginatedList);
		}
        #endregion
        #region Patient Create Appointment
        public IActionResult Create(ClinicAvailabilityViewModel model)
        {
            // Set the default year and month to the current if not provided
            DateTime today = DateTime.Now;
            if (model.SelectedYear == 0) model.SelectedYear = today.Year;
            if (model.SelectedMonth == 0) model.SelectedMonth = today.Month;

            // If clinicId is provided, fetch available appointments
            if (model.SelectedClinicId.HasValue)
            {
                var doctors = doctorRepo.Find(d => d.ClinicId == model.SelectedClinicId.Value).ToList();

                var availableDays = doctorScheduleRepo.Find(ds => doctors.Select(d => d.Id).Contains(ds.Id)).GroupBy(ds => ds.Day);

                int daysInCurrentMonth = DateTime.DaysInMonth(model.SelectedYear, model.SelectedMonth);

                List<DayAvailability> availabilityList = new List<DayAvailability>();

                // Generate availability data for each day in the selected month
                for (int i = 0; i < daysInCurrentMonth; i++)
                {
                    DayAvailability day = new DayAvailability();
                    day.Date = new DateTime(model.SelectedYear, model.SelectedMonth, i + 1);

                    // Check if the date is available
                    if (day.Date <= today)
                    {
                        day.IsAvailable = false;
                        day.AvailableDoctors = null;
                    }
                    else
                    {
                        foreach (var avaday in availableDays)
                        {
                            if (avaday.Key == day.Date.DayOfWeek)
                                foreach (var item in avaday)
                                {
                                    var count = appointmentRepo
                                        .Find(a => a.ApointmentDate == DateOnly.FromDateTime(day.Date)
                                              && a.ApointmentTime < item.StartTime
                                              && a.ApointmentTime < item.EndTime).Count();
                                    if (count < 50)
                                    {
                                        day.IsAvailable = true;
                                        if (day.AvailableDoctors == null)
                                        {
                                            day.AvailableDoctors = new List<DoctorScheduleLookup>();
                                        }
                                        day.AvailableDoctors.Add(item);

                                    }
                                }
                        }
                    }

                    availabilityList.Add(day);
                }

                model.AvailableDays = availabilityList;
            }

            // Set the selected year and month in the model
            model.SelectedYear = model.SelectedYear;
            model.SelectedMonth = model.SelectedMonth;
            model.SelectedClinicId = model.SelectedClinicId;

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmAppointment(ApointmentCreateVM model)
        {
            model.PatientId = 1;
            if (ModelState.IsValid)
            {
                appointmentRepo.Add(new Apointment().ConvertApointmentCreateVMToApointment(model));

                TempData["SuccessMessage"] = "Appointment booked successfully!";
                return RedirectToAction("Index");
            }
            // If no date was selected or there was an issue
            TempData["ErrorMessage"] = "Please select a valid date and time for booking.";
            return RedirectToAction("Create", new { clinicId = model.ClinicId });
        } 
        #endregion
    }
}





