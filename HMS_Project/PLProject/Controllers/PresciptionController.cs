using BLLProject.Interfaces;
using BLLProject.Repositories;
using DALProject.Data.Contexts;
using DALProject.Data.Migrations;
using DALProject.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels;
using PLProject.ViewModels.PrescriptionVM;
using X.PagedList;

namespace PLProject.Controllers
{
	[Authorize(Roles = $"{Roles.Admin}, {Roles.Pharmacist}")]
	public class PrescriptionController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		#region DPI
		private readonly IWebHostEnvironment env;

		public PrescriptionController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
		{
			this.unitOfWork = unitOfWork;
			env = _env;
		}
		#endregion

		public IActionResult Index(string searchQuery, int? page)
		{
			
			IEnumerable<Prescription> prescriptions;
			// Filter by ActiveSubstanceName (if provided)
			if (!string.IsNullOrEmpty(searchQuery))
			{
				prescriptions = unitOfWork.Repository<Prescription>().Find(p => p.Apointment.ApointmentDate == DateOnly.FromDateTime(DateTime.Now)
				&& p.Apointment.Patient.FullName.ToUpper().Contains(searchQuery.ToUpper())).AsNoTracking().ToList();
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
		}
		#region Create 
		public IActionResult Create()
		{
			return View(new PrescriptionViewModel());
		}

		[HttpPost]
		public IActionResult Create(PrescriptionViewModel model)
		{
			if (!model.HasItems)
			{
				ModelState.AddModelError(string.Empty, "You must add at least one item.");
			}
			else if (ModelState.IsValid)
			{
				var prescription = new Prescription()
				{
					PrescriptionItems = model.PrescriptionItems.Select(pi => pi.PrescriptionItemDoctorVMToPrescriptionItem()).ToList(),
				};
				prescription.DoctorId = 2;
				try
				{
					unitOfWork.Repository<Prescription>().Add(prescription);
					unitOfWork.Complete();
					// Update the active substance in the repository
				}
				catch (Exception ex)
				{
					// Handle exceptions and add error messages to the model state
					var errorMessage = env.IsDevelopment() ? ex.Message : "An error occurred during the update.";
					ModelState.AddModelError(string.Empty, errorMessage);
					return View(model);
				}
			}
			// If the model is invalid, return the view with the same model to show errors
			return View(model);
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

		[HttpPost]
		public IActionResult Edit(PrescriptionViewModel viewModel)
		{

			// If the model is invalid, repopulate lists and return the view
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			try
			{
				var updatedPrescription = unitOfWork.Repository<Prescription>().Get(viewModel.prescriptionId);

				//// Update the Prescription
				
				updatedPrescription.ConvertPrescriptionViewModelToPresciption(viewModel);
				
				unitOfWork.Repository<Prescription>().Update(updatedPrescription);
				unitOfWork.Complete();

				return RedirectToAction(nameof(Index), new { Id = viewModel.prescriptionId });
			}
			catch (Exception ex)
			{
				// Handle exceptions and add error messages to the model state
				var errorMessage = env.IsDevelopment() ? ex.Message : "An error occurred during the update.";
				ModelState.AddModelError(string.Empty, errorMessage);
				return View(viewModel);
			}
		}
		#endregion
	}
}
