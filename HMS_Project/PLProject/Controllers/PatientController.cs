using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.AspNetCore.Authorization;

namespace PLProject.Controllers
{
	public class PatientController : Controller
	{
		#region DPi
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public PatientController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
		{
			this.unitOfWork = unitOfWork;
			env = _env;
		}

		#endregion

		#region Idnex
		public IActionResult Index()
		{
			var Patients = unitOfWork.Repository<Patient>().GetALL();
			var PatientViewModels = Patients.Select(p => (PatientViewModel)p).ToList();
			return View(PatientViewModels);
		}
		#endregion

		#region Details
		[Authorize(Roles = Roles.Admin + "," + Roles.Patient)]
		[Route("Patient/Details/{userId}")]
		public IActionResult Details(string userId)
		{
			if (userId is null)
				return BadRequest(); // 400

			var Patient = unitOfWork.Repository<Patient>().Get(userId);

			if (Patient is null)
				return NotFound(); // 404

			var PatientViewModel = (PatientViewModel)Patient;

			return View(PatientViewModel);
		}
		#endregion

		#region Edit
		[Authorize(Roles = Roles.Admin + "," + Roles.Patient)]
		[Route("Patient/Edit/{userId}")]
		public IActionResult Edit(string userId)
		{
			if (userId is null)
				return BadRequest(); // 400

			var Patient = unitOfWork.Repository<Patient>().Get(userId);

			if (Patient is null)
				return NotFound(); // 404

			var PatientViewModel = (PatientViewModel)Patient;
			return View(PatientViewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		[Route("Patient/Edit/{userId}")]
		public IActionResult Edit([FromRoute] string userId, PatientViewModel ViewModel)
		{
			if (userId != ViewModel.UserId)
				return BadRequest();//400
			var patient = unitOfWork.Repository<Patient>().Get(ViewModel.UserId);
			if (ModelState.IsValid)
			{
				try
				{
					patient.UpdateInfo(ViewModel);
					//unitOfWork.Repository<Doctor>().Update(doctor);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "patient update successfully!";
					if (User.IsInRole(Roles.Patient))
						return RedirectToAction(nameof(Index), controllerName: "Home");
					else
						return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during update patient";
					return View(ViewModel);
				}
			}

			return View(ViewModel);
		}
		#endregion
	}
}
