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
	private readonly IWebHostEnvironment env;
	private readonly HMSdbcontextProcedures procedures;
	private readonly IUnitOfWork unitOfWork;
	#region DPI


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

		var viewModel = new ActiveSubstanceViewModel()
		{
			ActiveSubstancesDateReader = (unitOfWork.Repository<ActiveSubstance>().GetALL()),
			MedicationsDateReader = unitOfWork.Repository<Medication>().GetALL(),
		};
		return View(viewModel);

	}

	// POST: Handle form submission
	[HttpPost]
	public IActionResult Create(ActiveSubstanceViewModel model)
	{
		foreach (var MedId in model.MedicationId)
		{
			model.Medications.Add(unitOfWork.Repository<Medication>().Get(MedId));
		}
		if (ModelState.IsValid)
		{

			unitOfWork.Repository<ActiveSubstance>().Add((ActiveSubstance)model);
			unitOfWork.Complete();

			return RedirectToAction("Success"); // Redirect after successful creation
		}

		// Reload the lists if the model state is invalid
		model.ActiveSubstancesDateReader = (unitOfWork.Repository<ActiveSubstance>().GetALL());
		model.MedicationsDateReader = unitOfWork.Repository<Medication>().GetALL();

		return View(model);
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
		if (viewname == "Edit")
		{
			//get Activesubstance that are not exist on this substance 
			substancevm.ActiveSubstancesDateReader = unitOfWork.Repository<ActiveSubstance>().Find(x => !substancevm.Interactions.Select(i => i.ActSubId).Contains(x.Id));
			//get Medication that are not exist on this substance 
			substancevm.MedicationsDateReader = unitOfWork.Repository<Medication>().Find(x => !substancevm.Medications.Select(m => m.Id).Contains(x.Id));
		}

		return View(viewname, substancevm);
	}
	#endregion

	#region Delete
	public IActionResult Delete(int? Id)
	{
		return Details(Id, "Delete");
	}

	[HttpPost]
	public async Task<ActionResult> Delete(ActiveSubstanceViewModel substance)
	{
		try
		{

			await procedures.sp_DeleteActiveSubstanceAsync(substance.Id);

			return RedirectToAction(nameof(Index));
		}
		catch (Exception ex)
		{

			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");

			return View(substance);
		}
	}
	#endregion

	#region Edit
	public IActionResult Edit(int? Id)
	{
		return Details(Id, "Edit");
	}

	[HttpPost]
	public IActionResult Edit(ActiveSubstanceViewModel substance)
	{
		// Add medications associated with the substance
		substance.Medications.AddRange(unitOfWork.Repository<Medication>().Find(x => substance.MedicationId.Contains(x.Id)));
		unitOfWork.Complete();

		// Get the active substance from the repository
		var activeSubstance = unitOfWork.Repository<ActiveSubstance>().Get(substance.Id);

		// Add New medication to the active substance
		activeSubstance.Medications.AddRange(substance.Medications);
		unitOfWork.Complete();
		// Add New interactions to the active substance
		activeSubstance.ActSub1.AddRange(((ActiveSubstance)substance).ActSub1);
		unitOfWork.Complete();
		activeSubstance.ActiveSubstancesName = substance.ActiveSubstancesName;

		// If the model is invalid, repopulate lists and return the view
		if (!ModelState.IsValid)
		{
			// Get active substances that are not already part of this substance's interactions
			substance.ActiveSubstancesDateReader = unitOfWork.Repository<ActiveSubstance>().Find(
				x => !substance.Interactions.Select(i => i.ActSubId).Contains(x.Id));

			// Get medications that are not already part of this substance
			substance.MedicationsDateReader = unitOfWork.Repository<Medication>().Find(
				x => !substance.Medications.Select(m => m.Id).Contains(x.Id));

			return View(substance);
		}

		try
		{
			// Update the active substance in the repository
			unitOfWork.Repository<ActiveSubstance>().Update(activeSubstance);
			unitOfWork.Complete();
			return RedirectToAction(nameof(Edit), new { Id = substance.Id });
		}
		catch (Exception ex)
		{
			// Handle exceptions and add error messages to the model state
			var errorMessage = env.IsDevelopment() ? ex.Message : "An error occurred during the update.";
			ModelState.AddModelError(string.Empty, errorMessage);

			return View(substance);
		}
	}

	#region Edit Active Substance Interation 
	public IActionResult ActSubstEdit(int? ActId, int? InteractId, string Interaction)
	{
		if (!ActId.HasValue || !InteractId.HasValue)
			return BadRequest(); // 400

		var substance = unitOfWork.Repository<ActiveSubstance>().Get(ActId.Value);

		if (substance is null)
			return NotFound(); // 404

		var interaction = substance.ActSub1.Where(ai => ai.ActiveSubstanceId2 == InteractId).FirstOrDefault() ??
			substance.ActSub2.Where(ai => ai.ActiveSubstanceId1 == InteractId).FirstOrDefault();

		if (interaction is null)
			return NotFound(); // 404

		try
		{
			interaction.Interaction = Interaction;
			unitOfWork.Repository<ActiveSubstanceInteraction>().Update(interaction);
			// Redirect to Edit action and pass ActId as route parameter
			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
		catch (Exception ex)
		{

			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				ModelState.AddModelError(string.Empty, "An Error Has Occurred during editing the Department");

			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
	}
	#endregion
	#region delete Active Substance Interation 
	[HttpPost]
	public IActionResult ActSubstDelete(int? ActId, int? InteractId)
	{
		if (!ActId.HasValue || !InteractId.HasValue)
			return BadRequest(); // 400


		try
		{
			var substance = unitOfWork.Repository<ActiveSubstance>().Get(ActId.Value);

			if (substance is null)
				return NotFound(); // 404

			var interaction = substance.ActSub1.Where(ai => ai.ActiveSubstanceId2 == InteractId).FirstOrDefault() ??
				substance.ActSub2.Where(ai => ai.ActiveSubstanceId1 == InteractId).FirstOrDefault();

			if (interaction is null)
				return NotFound(); // 404

			unitOfWork.Repository<ActiveSubstanceInteraction>().Delete(interaction);
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
	#region Edit Medication in Active Substance  
	public IActionResult MedicationEdit(int? ActId, int? MedId, int Strength)
	{
		if (!ActId.HasValue || !MedId.HasValue)
			return BadRequest(); // 400

		var substance = unitOfWork.Repository<ActiveSubstance>().Get(ActId.Value);

		if (substance is null)
			return NotFound(); // 404

		var med = substance.Medications.Where(m => m.Id == MedId).FirstOrDefault();

		if (med is null)
			return NotFound(); // 404

		try
		{
			med.Strength = Strength;
			unitOfWork.Repository<Medication>().Update(med);
			unitOfWork.Complete();
			// Redirect to Edit action and pass ActId as route parameter
			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
		catch (Exception ex)
		{
			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				ModelState.AddModelError(string.Empty, "An Error Has Occurred during editing the Department");

			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
	}
	#endregion
	#region delete Medication from Active Substance  
	[HttpPost]
	public IActionResult MedicationDelete(int? ActId, int? MedId)
	{
		if (!ActId.HasValue || !MedId.HasValue)
			return BadRequest(); // 400

		var substance = unitOfWork.Repository<ActiveSubstance>().Get(ActId.Value);

		if (substance is null)
			return NotFound(); // 404

		var med = substance.Medications.Where(m => m.Id == MedId).FirstOrDefault();

		if (med is null)
			return NotFound(); // 404

		try
		{
			unitOfWork.Repository<Medication>().Delete(med);
			unitOfWork.Complete();
			// Redirect to Edit action and pass ActId as route parameter
			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
		catch (Exception ex)
		{
			if (env.IsDevelopment())
				ModelState.AddModelError(string.Empty, ex.Message);
			else
				ModelState.AddModelError(string.Empty, "An Error Has Occurred during deleting the Department");

			return RedirectToAction(nameof(Edit), new { Id = ActId });
		}
	}
	#endregion
	#endregion


}
