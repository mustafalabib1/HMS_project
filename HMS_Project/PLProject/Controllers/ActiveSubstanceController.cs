using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

public class ActiveSubstanceController : Controller
{
    #region DPI
    private readonly IRepository<ActiveSubstance> ActiveSubstanceRepo;

    private readonly IRepository<Medication> MedicationRepo;
    private readonly IWebHostEnvironment env;
	private readonly HMSdbcontextProcedures procedures;
	private readonly IRepository<ActiveSubstanceInteraction> ActInteractRepo;

	public ActiveSubstanceController(IRepository<ActiveSubstance> ActiveSubstanceRepo, IRepository<Medication> MedicationRepo, IWebHostEnvironment _env,
		HMSdbcontextProcedures procedures,IRepository<ActiveSubstanceInteraction> ActInteractRepo)
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
        // Fetch all ActiveSubstance entries
        var substances = ActiveSubstanceRepo.GetALL().ToList();

        // Map ActiveSubstance to ActiveSubstanceViewModel
        var ActSubVM = substances.Select(a => (ActiveSubstanceViewModel)a).ToList();

        // Filter by ActiveSubstanceName (if provided)
        if (!string.IsNullOrEmpty(searchQuery))
        {
            ActSubVM = ActSubVM.Where(s => s.ActiveSubstancesName
            .Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
        }

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

        var substance = (ActiveSubstanceViewModel)ActiveSubstanceRepo.Get(Id.Value);

        if (substance is null)
            return NotFound(); // 404

        return View(viewname, substance);
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
            //1. log exception
            //2. friendly message
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
    public IActionResult Edit(ActiveSubstance substance)
    {
        if (!ModelState.IsValid)
            return View(substance);

        try
        {
            ActiveSubstanceRepo.Update(substance);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {

            if (env.IsDevelopment())
                ModelState.AddModelError(string.Empty, ex.Message);
            else
                ModelState.AddModelError(string.Empty, "An Error Has Occurred during updating the ActiveSubstance ");

            return View(substance);
        }
    }
    #endregion

    #region Edite Active Substance Interation  
    [HttpPost]
	public IActionResult ActSubstDelete(int? ActId, int ? InteractId)
	{
		if (!ActId.HasValue || !InteractId.HasValue)
			return BadRequest(); // 400

		var substance = ActiveSubstanceRepo.Get(ActId.Value);

		if (substance is null)
			return NotFound(); // 404

        var interaction= substance.ActSub1.Where(ai=>ai.ActiveSubstanceId2==InteractId).FirstOrDefault()??
            substance.ActSub2.Where(ai => ai.ActiveSubstanceId1 == InteractId).FirstOrDefault();

  		if (interaction is null)
			return NotFound(); // 404

		ActInteractRepo.Delete(interaction);


		// Redirect to Edit action and pass ActId as route parameter
		return RedirectToAction(nameof(Edit), new { Id = ActId });
	}
	#endregion
}
