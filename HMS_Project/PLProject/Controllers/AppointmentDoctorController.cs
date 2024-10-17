using BLLProject.Interfaces;
using BLLProject.Specification;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels;
using PLProject.ViewModels.AppointmentViewModel;
using PLProject.ViewModels.PrescriptionVM;
using X.PagedList;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Doctor)]
	[Authorize(Roles = Roles.Admin)]

	public class AppointmentDoctorController : Controller
	{
		#region Dpi
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;
		private readonly UserManager<AppUser> userManager;

		public AppointmentDoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment Env, UserManager<AppUser> UserManager)
		{
			this.unitOfWork = unitOfWork;
			env = Env;
			userManager = UserManager;
		} 
		#endregion


		#region Get all Appointment for Doctor 
		public async Task<IActionResult> IndexAsync(int? page )
		{
			//         // Get the current doctor ID
			//         var user = await userManager.GetUserAsync(User);

			//         int DoctorId = 2;//user?.Id; 
			//         var appointments = unitOfWork.Repository<Apointment>().Find(a => a.DoctorId == DoctorId/*&&a.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)*/)
			//	.Include(a => a.Patient).Include(a => a.Doctor).Include(a => a.Clinic).ToList();

			//var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
			//// Pagination logic
			//int pageSize = 10;
			//int pageNumber = page ?? 1;

			//var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

			//return View(paginatedList);
			return View();
		}

		#endregion

		#region Details
		public IActionResult Details(int? Id, string viewname = "Details")
		{
			//if (!Id.HasValue)
			//	return BadRequest(); // 400
			//var spec= new BaseSpecification<Apointment>(a=>a.Id==Id);
			//spec.Includes.Add(a=>a.Patient);
			//spec.Includes.Add(a=>a.Doctor);
			//spec.Includes.Add(a=>a.Clinic);
			//spec.Includes.Add(a => a.Prescription);
			//var apointment = unitOfWork.Repository<Apointment>().GetEntityWithSpec(spec);
			//var apointmentVM= apointment.ConvertApointmentToAppointmentGenarelVM();

			//if (apointmentVM is null)
			//	return NotFound(); // 404

			//return View(viewname, apointmentVM);
			return View();

		}
		#endregion

		#region Edit
		public IActionResult Edit(int? Id)
		{
			return Details(Id, "Edit");
		}

		[HttpPost]
		public IActionResult Edit(AppointmentGenarelVM ViewModel)
		{

			// If the model is invalid, repopulate lists and return the view
			if (!ModelState.IsValid)
			{
				return View(ViewModel);
			}

			try
			{

                // get the appointment from Repository 
                var apointment = unitOfWork.Repository<Apointment>().Get(ViewModel.Id);
				apointment.ConvertAppointmentGenarelVMToApointment(ViewModel);

				// Update the appointment in the repository
				unitOfWork.Repository<Apointment>().Update(apointment);
				unitOfWork.Complete();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				// Handle exceptions and add error messages to the model state
				var errorMessage = env.IsDevelopment() ? ex.Message : "An error occurred during the update.";
				ModelState.AddModelError(string.Empty, errorMessage);

				return View(ViewModel);
			}
		}

		#endregion

	}
}
