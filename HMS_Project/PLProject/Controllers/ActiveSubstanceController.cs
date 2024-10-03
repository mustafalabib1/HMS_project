using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ActiveSubstanceController : Controller
{
    private readonly HMSdbcontext Context;

    public ActiveSubstanceController(HMSdbcontext context)
    {
        Context = context;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(Create));
    }
    public IActionResult Create()
    {
        var viewModel = new ActiveSubstanceViewModel()
        {
            ActiveSubstancesDateReader = Context.ActiveSubstances.ToList(),
            MedicationsDateReader = Context.Medication.ToList()
        };
        return View(viewModel);
    }

    // POST: Handle form submission
    [HttpPost]
    public IActionResult Create(ActiveSubstanceViewModel model)
    {
        if (ModelState.IsValid)
        {
            
            Context.ActiveSubstances.Add((ActiveSubstance)model);
            Context.SaveChanges();

            return RedirectToAction("Success"); // Redirect after successful creation
        }

        // Reload the lists if the model state is invalid
        model.ActiveSubstancesDateReader = Context.ActiveSubstances.ToList();
        model.MedicationsDateReader = Context.Medication.ToList();

        return View(model);
    }

    // Success action
    public IActionResult Success()
    {
        return View();
    }
}
