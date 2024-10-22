using BLLProject.Interfaces;
using BLLProject.Repositories;
using DALProject.Data.Contexts;
using DALProject.Data.Migrations;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels;
using PLProject.ViewModels.PrescriptionVM;
using X.PagedList;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Pharmacist)]
	public class PrescriptionController : Controller
	{
        #region DPI
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<AppUser> _userManager; // Add UserManager

        public PrescriptionController(IUnitOfWork unitOfWork, IWebHostEnvironment _env, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            env = _env;
            _userManager = userManager; // Initialize UserManager
        }
        #endregion

        #region Index
        public IActionResult Index(string searchQuery, int? page)
		{

			IEnumerable<Prescription> prescriptions;
			// Filter by ActiveSubstanceName (if provided)
			if (!string.IsNullOrEmpty(searchQuery))
			{
				prescriptions = unitOfWork.Repository<Prescription>().Find(p => p.Apointment.ApointmentDate == DateOnly.FromDateTime(DateTime.Now)
				&& p.Apointment.Patient.AppUser.FullName.ToUpper().Contains(searchQuery.ToUpper())).AsNoTracking().ToList();
			}
			else
			{
				// Fetch all prescriptions entries for this day 
				prescriptions = unitOfWork.Repository<Prescription>()./*Find(p=>p.Apointment.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)).AsNoTracking()*/GetALL().ToList();
			}
			var prescriptionsVM = prescriptions.Select(p => p.ConvertPresciptionToPrescriptionViewModel());
			// Pagination logic
			int pageSize = 10;
			int pageNumber = page ?? 1;

			ViewData["CurrentFilter"] = searchQuery;
			var paginatedList = prescriptionsVM.ToPagedList(pageNumber, pageSize);
			return View(paginatedList);
			return View();

		}
		#endregion

		#region Details
		public IActionResult Details(int? Id, string viewname = "Details")
		{
			if (!Id.HasValue)
				return BadRequest(); // 400

			var prescription = unitOfWork.Repository<Prescription>().Get(Id.Value);
			var prescriptionVM = prescription.ConvertPresciptionToPrescriptionViewModel();

			if (prescriptionVM is null)
				return NotFound(); // 404

			return View(viewname, prescriptionVM);
		}
		#endregion

		#region Edit
		public IActionResult Edit(int? Id)
		{
			return Details(Id, "Edit");
		}

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id, PrescriptionViewModel ViewModel)
		{

            if (Id != ViewModel.prescriptionId)
                return BadRequest();//400
            ModelState.Remove<PrescriptionViewModel>(p => p.DoctorUserId);
            // If the model is invalid, repopulate lists and return the view
            if (!ModelState.IsValid)
			{
				return View(ViewModel);
			}

			try
			{
				var user = _userManager.GetUserAsync(User).GetAwaiter().GetResult();
				var updatedPrescription = unitOfWork.Repository<Prescription>().Get(ViewModel.prescriptionId);
				updatedPrescription.PharmacistUserId = user.Id;

				//// Update the Prescription
				
				updatedPrescription.ConvertPrescriptionViewModelToPresciption(ViewModel);
				
				unitOfWork.Repository<Prescription>().Update(updatedPrescription);
				unitOfWork.Complete();


				// Set a success message using TempData
				TempData["SuccessMessage"] = "Prescription update successfully!";

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
