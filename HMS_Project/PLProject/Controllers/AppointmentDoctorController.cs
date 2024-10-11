using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels.AppointmentViewModel;
using X.PagedList;

namespace PLProject.Controllers
{
	public class AppointmentDoctorController : Controller
	{
        private readonly IUnitOfWork unitOfWork;



        public AppointmentDoctorController( IUnitOfWork unitOfWork)
		{
            this.unitOfWork = unitOfWork;
       
        }
		

		#region Get all Appointment for patient 
		public IActionResult Index(int? page, int DoctorId = 2)
		{
			var appointments = unitOfWork.Repository<Apointment>().Find(a => a.DoctorId == DoctorId/*&&a.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)*/)
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
