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

    public ActiveSubstanceController(IRepository<ActiveSubstance> ActiveSubstanceRepo, IRepository<Medication> MedicationRepo, IWebHostEnvironment _env)
    {
        this.ActiveSubstanceRepo = ActiveSubstanceRepo;
        this.MedicationRepo = MedicationRepo;
        env = _env;
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
    public IActionResult Delete(ActiveSubstanceViewModel substance)
    {
        try
        {

            ActiveSubstanceRepo.Delete(ActiveSubstanceRepo.Get(substance.Id));
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
}
