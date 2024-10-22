using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[Authorize(Roles = $"{Roles.Admin}, {Roles.Pharmacist}")]
public class ActiveSubstanceController : Controller
{
	#region DPI

	private readonly IWebHostEnvironment env;
	private readonly HMSdbcontextProcedures procedures;
	private readonly IUnitOfWork unitOfWork;

	public ActiveSubstanceController(IWebHostEnvironment _env, HMSdbcontextProcedures procedures, IUnitOfWork unitOfWork)
	{

		env = _env;
		this.procedures = procedures;
		this.unitOfWork = unitOfWork;

	}

	#endregion

	#region Index
	public IActionResult Index(string searchQuery, int? page)
	{

		IEnumerable<ActiveSubstance> susbstances;
		// Filter by ActiveSubstanceName (if provided)
		if (!string.IsNullOrEmpty(searchQuery))
		{
			susbstances = unitOfWork.Repository<ActiveSubstance>().Find(s => s.ActiveSubstancesName.ToUpper().Contains(searchQuery.ToUpper())).AsNoTracking().ToList();
		}
		else
		{
			// Fetch all ActiveSubstance entries

			susbstances = unitOfWork.Repository<ActiveSubstance>().GetALL();
		}

		// Map ActiveSubstance to ActiveSubstanceViewModel
		var ActSubVM = susbstances.Select(a => (ActiveSubstanceViewModel)a).ToList();
		// Pagination logic
		int pageSize = 10;
		int pageNumber = page ?? 1;

		ViewData["CurrentFilter"] = searchQuery;
		var paginatedList = ActSubVM.ToPagedList(pageNumber, pageSize);

		return View(paginatedList);
	}
	#endregion

	#region Create
	public IActionResult Create()
	{
		return View();
	}

	// POST: Handle form submission
	[HttpPost]
	public IActionResult Create(ActiveSubstanceViewModel ViewModel)
	{
		foreach (var MedId in ViewModel.MedicationId)
		{
			ViewModel.Medications.Add(unitOfWork.Repository<Medication>().Get(MedId));
		}
		if (ModelState.IsValid)
		{
			try
			{
				unitOfWork.Repository<ActiveSubstance>().Add((ActiveSubstance)ViewModel);
				unitOfWork.Complete();
				// Set a success message using TempData
				TempData["SuccessMessage"] = "Active substance Created  successfully!";

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					// Set an error message using TempData
					TempData["ErrorMessage"] = "An Error Has Occurred during Createing Active substance.";
				return View(ViewModel);
			}
		}
		return View(ViewModel);
	}

	// Success action
	public IActionResult Success()
	{
		return View();
	}
	#endregion

	#region Details
	public IActionResult Details(int? Id, string viewname = "Details")
	{
		if (!Id.HasValue)
			return BadRequest(); // 400
		var substandce = unitOfWork.Repository<ActiveSubstance>().Get(Id.Value);
		var substancevm = (ActiveSubstanceViewModel)substandce;

		if (substancevm is null)
			return NotFound(); // 404
		return View(viewname, substancevm);
	}
	#endregion

	#region Delete
	public IActionResult Delete(int? Id)
	{
		return Details(Id, "Delete");
	}

	[HttpPost, ValidateAntiForgeryToken]
	public async Task<ActionResult> Delete([FromRoute] int Id, ActiveSubstanceViewModel viewModel)
	{
		if (Id != viewModel.Id)
			return BadRequest();//400
		try
		{

			await procedures.sp_DeleteActiveSubstanceAsync(viewModel.Id);

			// Set a success message using TempData
			TempData["SuccessMessage"] = "Active Substance delete successfully!";

			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{

			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Active Substance");

			return View(viewModel);
		}
	}
	#endregion

	#region Edit
	public IActionResult Edit(int? Id)
	{
		return Details(Id, "Edit");
	}

	[HttpPost, ValidateAntiForgeryToken]
	public IActionResult Edit([FromRoute] int Id, ActiveSubstanceViewModel viewModel)
	{

		if (Id != viewModel.Id)
			return BadRequest();//400
								// Add medications associated with the substance
		viewModel.Medications.AddRange(unitOfWork.Repository<Medication>().Find(x => viewModel.MedicationId.Contains(x.Id)));
		unitOfWork.Complete();

		// Get the active substance from the repository
		var activeSubstance = unitOfWork.Repository<ActiveSubstance>().Get(viewModel.Id);

		// Add New medication to the active substance
		activeSubstance.Medications.AddRange(viewModel.Medications);
		unitOfWork.Complete();
		// Add New interactions to the active substance
		activeSubstance.ActSub1.AddRange(((ActiveSubstance)viewModel).ActSub1);
		unitOfWork.Complete();
		activeSubstance.ActiveSubstancesName = viewModel.ActiveSubstancesName;

		// If the model is invalid, repopulate lists and return the view
		if (!ModelState.IsValid)
		{
			return View(viewModel);
		}

		try
		{
			// Update the active substance in the repository
			unitOfWork.Repository<ActiveSubstance>().Update(activeSubstance);
			unitOfWork.Complete();

			// Set a success message using TempData
			TempData["SuccessMessage"] = "Active Substance update successfully!";

			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{
			// Handle exceptions and add error messages to the model state

			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				// Set an error message using TempData
				TempData["ErrorMessage"] = "An Error Has Occurred during the update.";

			return View(viewModel);
		}
	}
	#endregion
}
