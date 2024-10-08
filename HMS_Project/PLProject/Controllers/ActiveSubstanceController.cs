using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System;

public class ActiveSubstanceController : Controller
{
    #region DPI
    private readonly IRepository<ActiveSubstance> ActiveSubstanceRepo;

    private readonly IRepository<Medication> MedicationRepo;
    private readonly IWebHostEnvironment env;
    private readonly HMSdbcontextProcedures procedures;
    private readonly IRepository<ActiveSubstanceInteraction> ActInteractRepo;

    public ActiveSubstanceController(
        IRepository<ActiveSubstance> ActiveSubstanceRepo,
        IRepository<Medication> MedicationRepo, IWebHostEnvironment _env,
        HMSdbcontextProcedures procedures,
        IRepository<ActiveSubstanceInteraction> ActInteractRepo)
    {
        this.ActiveSubstanceRepo = ActiveSubstanceRepo;
        this.MedicationRepo = MedicationRepo;
        env = _env;
        this.procedures = procedures;
        this.ActInteractRepo = ActInteractRepo;
    }

    #endregion

    #region Index
    public IActionResult Index(string searchQuery, int? page)
    {

         IEnumerable<ActiveSubstance> susbstances;
        // Filter by ActiveSubstanceName (if provided)
        if (!string.IsNullOrEmpty(searchQuery))
        {
           susbstances = ActiveSubstanceRepo.Find(s => s.ActiveSubstancesName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
             // Fetch all ActiveSubstance entries
            susbstances=ActiveSubstanceRepo.GetALL();
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
            ActiveSubstancesDateReader = (ActiveSubstanceRepo.GetALL()),
            MedicationsDateReader = MedicationRepo.GetALL(),
        };
        return View(viewModel);
    }

    // POST: Handle form submission
    [HttpPost]
    public IActionResult Create(ActiveSubstanceViewModel model)
    {
        foreach (var MedId in model.MedicationId)
        {
            model.Medications.Add(MedicationRepo.Get(MedId));
        }
        if (ModelState.IsValid)
        {

            ActiveSubstanceRepo.Add((ActiveSubstance)model);

            return RedirectToAction("Success"); // Redirect after successful creation
        }

        // Reload the lists if the model state is invalid
        model.ActiveSubstancesDateReader = (ActiveSubstanceRepo.GetALL());
        model.MedicationsDateReader = MedicationRepo.GetALL();

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
        var substandce = ActiveSubstanceRepo.Get(Id.Value);
        var substancevm = (ActiveSubstanceViewModel)substandce;

        if (substancevm is null)
            return NotFound(); // 404
        if (viewname == "Edit")
        {
            //get Activesubstance that are not exist on this substance 
            substancevm.ActiveSubstancesDateReader = ActiveSubstanceRepo.Find(x => !substancevm.Interactions.Select(i => i.ActSubId).Contains(x.Id));
            //get Medication that are not exist on this substance 
            substancevm.MedicationsDateReader = MedicationRepo.Find(x => !substancevm.Medications.Select(m => m.Id).Contains(x.Id));
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
        substance.Medications.AddRange(MedicationRepo.Find(x => substance.MedicationId.Contains(x.Id)));

        // Get the active substance from the repository
        var activeSubstance = ActiveSubstanceRepo.Get(substance.Id);

        // Add New medication to the active substance
        activeSubstance.Medications.AddRange(substance.Medications);
        // Add New interactions to the active substance
        activeSubstance.ActSub1.AddRange(((ActiveSubstance)substance).ActSub1);
        activeSubstance.ActiveSubstancesName = substance.ActiveSubstancesName;

        // If the model is invalid, repopulate lists and return the view
        if (!ModelState.IsValid)
        {
            // Get active substances that are not already part of this substance's interactions
            substance.ActiveSubstancesDateReader = ActiveSubstanceRepo.Find(
                x => !substance.Interactions.Select(i => i.ActSubId).Contains(x.Id));

            // Get medications that are not already part of this substance
            substance.MedicationsDateReader = MedicationRepo.Find(
                x => !substance.Medications.Select(m => m.Id).Contains(x.Id));

            return View(substance);
        }

        try
        {
            // Update the active substance in the repository
            ActiveSubstanceRepo.Update(activeSubstance);
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

        var substance = ActiveSubstanceRepo.Get(ActId.Value);

        if (substance is null)
            return NotFound(); // 404

        var interaction = substance.ActSub1.Where(ai => ai.ActiveSubstanceId2 == InteractId).FirstOrDefault() ??
            substance.ActSub2.Where(ai => ai.ActiveSubstanceId1 == InteractId).FirstOrDefault();

        if (interaction is null)
            return NotFound(); // 404

        try
        {
            interaction.Interaction = Interaction;
            ActInteractRepo.Update(interaction);
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

        var substance = ActiveSubstanceRepo.Get(ActId.Value);

        if (substance is null)
            return NotFound(); // 404

        var interaction = substance.ActSub1.Where(ai => ai.ActiveSubstanceId2 == InteractId).FirstOrDefault() ??
            substance.ActSub2.Where(ai => ai.ActiveSubstanceId1 == InteractId).FirstOrDefault();

        if (interaction is null)
            return NotFound(); // 404

        ActInteractRepo.Delete(interaction);


        // Redirect to Edit action and pass ActId as route parameter
        return RedirectToAction(nameof(Edit), new { Id = ActId });
    }
    #endregion
    #region Edit Medication in Active Substance  
    public IActionResult MedicationEdit(int? ActId, int? MedId, int Strength)
    {
        if (!ActId.HasValue || !MedId.HasValue)
            return BadRequest(); // 400

        var substance = ActiveSubstanceRepo.Get(ActId.Value);

        if (substance is null)
            return NotFound(); // 404

        var med = substance.Medications.Where(m => m.Id == MedId).FirstOrDefault();

        if (med is null)
            return NotFound(); // 404

        try
        {
            med.Strength = Strength;
            MedicationRepo.Update(med);
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

        var substance = ActiveSubstanceRepo.Get(ActId.Value);

        if (substance is null)
            return NotFound(); // 404

        var med = substance.Medications.Where(m => m.Id == MedId).FirstOrDefault();

        if (med is null)
            return NotFound(); // 404

        try
        {
            MedicationRepo.Delete(med);
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
