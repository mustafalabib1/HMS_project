using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class MedicationController : Controller
    {
        private readonly IRepository<Medication> medicationRepo;

        public MedicationController(IRepository<Medication> medicationRepo)
        {
            this.medicationRepo = medicationRepo;
        }


        public IActionResult Index()
        {
            return RedirectToAction(nameof(Create));
        }

        #region Create
        public IActionResult Create()
        {
            var viewModel = new MedicationViewModel
            {
                MedicationId = Guid.NewGuid().ToString()

            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(MedicationViewModel medicationViewModel)
        {
            if (ModelState.IsValid)
            {
                var medication = new Medication
                {
                    MedicationId = medicationViewModel.MedicationId,
                    MedName = medicationViewModel.MedName,
                    Strength = medicationViewModel.Strength
                };

                medicationRepo.Add(medication);
                return RedirectToAction(nameof(Index));
            }

            return View(medicationViewModel);
        }
        #endregion
    }
}

