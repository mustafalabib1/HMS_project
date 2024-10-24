using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.ViewModels;

namespace PLProject.Controllers
{
	public class DoctorController : Controller
	{
		#region DPI
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
		{
			this.unitOfWork = unitOfWork;
			env = _env;
		}
		#endregion

		#region Index (List Doctors)
		[Authorize(Roles = Roles.Admin)]

		public IActionResult Index()
		{
			var doctors = unitOfWork.Repository<Doctor>().GetALL();
			var doctorViewModels = doctors.Select(d => (DoctorViewModel)d).ToList();
			return View(doctorViewModels);
		}
		#endregion

		#region Specialization
		[Authorize(Roles = Roles.Admin)]

		public IActionResult AddSpecialization()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddSpecialization(DoctorSpecializationLookup doctorSpecializationLookup)
		{
			if (ModelState.IsValid)
			{
				var spec = new BaseSpecification<DoctorSpecializationLookup>(ds => ds.Specialization == doctorSpecializationLookup.Specialization);
				var Specialization = unitOfWork.Repository<DoctorSpecializationLookup>().GetEntityWithSpec(spec);

				if (Specialization is null)
				{
					unitOfWork.Repository<DoctorSpecializationLookup>().Add(doctorSpecializationLookup);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "Specialization added successfully!";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					// Set an error message using TempData
					TempData["ErrorMessage"] = "This specialization already exists.";
					return View(doctorSpecializationLookup);
				}
			}

			return View(doctorSpecializationLookup);
		}
		#endregion


		#region Details
		[Authorize(Roles = Roles.Admin + "," + Roles.Doctor)]  // Admins can edit all, doctors can edit their own profile

		[Route("Doctor/Details/{userId}")]
		public IActionResult Details(string userId)
		{
			if (userId is null)
				return BadRequest(); // 400

			var spec = new BaseSpecification<Doctor>(e => e.UserId == userId);
			spec.Includes.Add(e => e.DoctorSpecialization);

			var doctor = unitOfWork.Repository<Doctor>().GetEntityWithSpec(spec);

			if (doctor is null)
				return NotFound(); // 404

			var doctorViewModel = (DoctorViewModel)doctor;

			return View(doctorViewModel);
		}
		#endregion

		#region Edit
		[Authorize(Roles = Roles.Admin + "," + Roles.Doctor)]  // Admins can edit all, doctors can edit their own profile
		[Route("Doctor/Edit/{userId}")]
		public IActionResult Edit(string userId)
		{
			if (userId is null)
				return BadRequest(); // 400

			var doctor = unitOfWork.Repository<Doctor>().Get(userId);
			unitOfWork.Complete();

			if (doctor is null)
				return NotFound(); // 404

			var doctorViewModel = (DoctorViewModel)doctor;

			unitOfWork.Complete();

			return View(doctorViewModel);
		}

		[Authorize(Roles = Roles.Admin + "," + Roles.Doctor)]  // Admins can edit all, doctors can edit their own profile
		[HttpPost, ValidateAntiForgeryToken]
		[Route("Doctor/Edit/{userId}")]
		public IActionResult Edit([FromRoute] string userId, DoctorViewModel ViewModel)
		{
			if (userId != ViewModel.UserId)
				return BadRequest();//400
			Doctor doctor = unitOfWork.Repository<Doctor>().Get(ViewModel.UserId);

			if (ModelState.IsValid)
			{
				try
				{
					doctor.UpdatedDoctor(ViewModel);
					//unitOfWork.Repository<Doctor>().Update(doctor);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "Doctor update successfully!";
					if (User.IsInRole(Roles.Doctor))
						return RedirectToAction(nameof(Index),controllerName:"Home");
					else 						
						return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during update doctor";
					return View(ViewModel);
				}
			}

			return View(ViewModel);

		}
		#endregion

		#region Delete Schedule day 
		[Authorize(Roles = Roles.Admin)]
		[HttpPost]
		public IActionResult DeleteScheduleDay(int ScheduleId)
		{
			try
			{
				// Find the schedule entry based on DoctorId and DayId
				var scheduleDay = unitOfWork.Repository<DoctorScheduleLookup>().Get(ScheduleId);

				if (scheduleDay == null)
				{
					return Json(new { success = false, message = "Schedule day not found." });
				}

				// Delete the schedule day
				unitOfWork.Repository<DoctorScheduleLookup>().Delete(scheduleDay);
				unitOfWork.Complete();

				// Return success response
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				// Return error response
				return Json(new { success = false, message = "Error occurred: " + ex.Message });
			}
		}
		#endregion
	}
}
