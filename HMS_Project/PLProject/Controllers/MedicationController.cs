using BLLProject.Interfaces;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLProject.Controllers
{
    public class MedicationController : Controller
    {
        private readonly IRepository<Medication> medicationRepository;
        private readonly IWebHostEnvironment env;

        public MedicationController(IRepository<Medication> medicationRepository, IWebHostEnvironment env)
        {
            this.medicationRepository = medicationRepository;
            this.env = env;
        }

        public IActionResult Index()
        {
            var medications = medicationRepository.GetALL();
            var medicationViewModels = medications.Select(m => new MedicationViewModel
            {

                MedName = m.MedName,
                Strength = m.Strength,

            }).ToList();
            return View(medicationViewModels);
        }

        public IActionResult Create()
        {
            var model = new MedicationViewModel
            {
                ActSubDateReader = new List<ActiveSubstance>(), // Ensure this is initialized
                //PrescriptionItemsReader = new List<Prescription>() // Initialize if needed
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

                };

                medicationRepository.Add(medication);
                return RedirectToAction(nameof(Index));
            }

            medicationViewModel.ActSubDateReader ??= new List<ActiveSubstance>();

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
                MedName = medication.MedName,
                Strength = medication.Strength,

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
                MedName = medicationViewModel.MedName,
                Strength = medicationViewModel.Strength,

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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var medication = medicationRepository.Get(id);

            if (medication == null)
                return NotFound();

            medicationRepository.Delete(medication);
            return RedirectToAction(nameof(Index));
        }
    }
}
