using BLLProject.Interfaces;
using BLLProject.Specification;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels;
using PLProject.ViewModels.AppointmentViewModel;
using PLProject.ViewModels.PrescriptionVM;
using X.PagedList;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Doctor)]

	public class AppointmentDoctorController : Controller
	{
		#region Dpi
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;
		private readonly UserManager<AppUser> userManager;
		private string UserId;

		public AppointmentDoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment Env, UserManager<AppUser> UserManager)
		{
			this.unitOfWork = unitOfWork;
			env = Env;
			userManager = UserManager;
		} 
		#endregion

		#region Get all Appointment for Doctor 
		public async Task<IActionResult> Index(int? page )
		{
			// Get the current doctor ID
			var user = await userManager.GetUserAsync(User);

			UserId = user?.Id?? string.Empty;

			var spec = new BaseSpecification<Apointment>(a => a.DoctorUserId == UserId/*&&a.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)*/&& a.ApointmentStatus == ApointmentStatusEnum.Confirmed);
			spec.Includes.Add(a => a.Patient);
			spec.Includes.Add(a => a.Doctor);
			spec.Includes.Add(a => a.Clinic);

			var appointments = unitOfWork.Repository<Apointment>().GetALLWithSpec(spec).ToList(); 

			var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
			// Pagination logic
			int pageSize = 10;
			int pageNumber = page ?? 1;

			var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

			return View(paginatedList);
		}

		#endregion

		#region Details
		public IActionResult Details(int? Id, string viewname = "Details")
		{
			if (!Id.HasValue)
				return BadRequest(); // 400
			var spec = new BaseSpecification<Apointment>(a => a.Id == Id);
			spec.Includes.Add(a => a.Patient);
			spec.Includes.Add(a => a.Doctor);
			spec.Includes.Add(a => a.Clinic);
			spec.Includes.Add(a => a.Prescription);
			var apointment = unitOfWork.Repository<Apointment>().GetEntityWithSpec(spec);
			var apointmentVM = apointment.ConvertApointmentToAppointmentGenarelVM();

			if (apointmentVM is null)
				return NotFound(); // 404

			return View(viewname, apointmentVM);

		}
		#endregion

		#region Edit
		public IActionResult Edit(int? Id)
		{
			return Details(Id, "Edit");
		}

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id,AppointmentGenarelVM ViewModel)
		{

            if (Id != ViewModel.Id)
                return BadRequest();//400
			// If the model is invalid, repopulate lists and return the view
            ModelState.Remove<AppointmentGenarelVM>(a => a.PrescriptionViewModel.DoctorUserId);

            if (!ModelState.IsValid)
			{
                return Details(ViewModel.Id, "Edit");
            }

			try
			{
                // get the appointment from Repository 
                var apointment = unitOfWork.Repository<Apointment>().Get(ViewModel.Id);
				apointment.ConvertAppointmentGenarelVMToApointment(ViewModel);

				apointment.ApointmentStatus= ApointmentStatusEnum.Completed;
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
