using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace PLProject.Controllers
{
    public class MedicationController : Controller
    {
        private readonly IRepository<Medication> medicationRepository;
        private readonly IRepository<ActiveSubstance> activeSubstanceRepository; // Assuming you have this repository
        private readonly IWebHostEnvironment env;

        public MedicationController(IRepository<Medication> medicationRepository, IRepository<ActiveSubstance> activeSubstanceRepository, IWebHostEnvironment env)
        {
            this.medicationRepository = medicationRepository;
            this.activeSubstanceRepository = activeSubstanceRepository; // Initialize the active substance repository
            this.env = env;
        }

        public IActionResult Index(string searchQuery, int? page)
        {
            var medications = medicationRepository.GetALL().ToList();
            var medicationVM = medications.Select(m => new MedicationViewModel
            {
                MedicationId = m.Id,
                MedName = m.MedName,
                Strength = m.Strength
            }).ToList();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                medicationVM = medicationVM.Where(m => m.MedName
                    .Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int pageSize = 12;
            int pageNumber = page ?? 1;
            ViewData["CurrentFilter"] = searchQuery;
            var paginatedList = medicationVM.ToPagedList(pageNumber, pageSize);

            return View(paginatedList);
        }

        public IActionResult Create()
        {
            var model = new MedicationViewModel
            {
                ActSubDateReader = activeSubstanceRepository.GetALL().ToList(), // Load active substances
                PrescriptionItemsReader = new List<PrescriptionItem>() // Assuming you have a way to get prescriptions
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(MedicationViewModel medicationViewModel)
        {
            if (ModelState.IsValid)
            {
                var medication = new Medication
                {
                    MedName = medicationViewModel.MedName,
                    Strength = medicationViewModel.Strength,
                    // Assuming you have a way to handle active substances and prescriptions
                };
                medicationRepository.Add(medication);
                return RedirectToAction(nameof(Index));
            }

            medicationViewModel.ActSubDateReader = activeSubstanceRepository.GetALL().ToList(); // Reload active substances
            return View(medicationViewModel);
        }

        public IActionResult Details(int id)
        {
            var medication = medicationRepository.Get(id);
            if (medication == null)
                return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                MedName = medication.MedName,
                Strength = medication.Strength,
            };

            return View(medicationViewModel);
        }

        public IActionResult Edit(int id)
        {
            var medication = medicationRepository.Get(id);
            if (medication == null)
                return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                MedicationId = medication.Id, // Assuming you have an Id property
                MedName = medication.MedName,
                Strength = medication.Strength,
                // Populate other necessary fields if needed
            };

            return View(medicationViewModel);
        }

        [HttpPost]
        public IActionResult Edit(MedicationViewModel medicationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(medicationViewModel);
            }

            var medication = new Medication
            {
                Id = medicationViewModel.MedicationId, // Ensure the ID is set correctly
                MedName = medicationViewModel.MedName,
                Strength = medicationViewModel.Strength,
                // Update other necessary fields if needed
            };

            try
            {
                medicationRepository.Update(medication);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An error occurred during the update.");

                return View(medicationViewModel);
            }
        }
    

        public IActionResult Delete(int id)
        {
            var medication = medicationRepository.Get(id);
            if (medication == null)
                return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                MedName = medication.MedName,
                Strength = medication.Strength,
                ActSubInMed = medication.ActiveSubstances // Assuming you have a way to get active substances related to the medication
            };

            return View(medicationViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var medication = medicationRepository.Get(id);
            if (medication == null)
                return NotFound();

            medicationRepository.Delete(medication);
            return RedirectToAction(nameof(Index));
        }

    }
}
