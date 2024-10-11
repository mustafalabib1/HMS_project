using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.Helpers;
using PLProject.ViewModels.AppointmentViewModel;
using X.PagedList;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Doctor)]
	public class AppointmentDoctorController : Controller
	{
		private readonly IRepository<Apointment> appointmentRepo;
		private readonly IRepository<Clinic> clinicRepo;
		private readonly IRepository<Receptionist> receptionistRepo;
		private readonly IRepository<Doctor> doctorRepo;
		private readonly IRepository<DoctorScheduleLookup> doctorScheduleRepo;

		public AppointmentDoctorController(IRepository<Apointment> appointmentRepo,
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
		public IActionResult Index(int? page, int DoctorId = 2)
		{
			var appointments = appointmentRepo.Find(a => a.DoctorId == DoctorId/*&&a.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)*/)
				.Include(a => a.Patient).Include(a => a.Doctor).Include(a => a.Clinic).ToList();

			var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
			// Pagination logic
			int pageSize = 10;
			int pageNumber = page ?? 1;

			var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

			return View(paginatedList);
		}

		#endregion
	}
}
