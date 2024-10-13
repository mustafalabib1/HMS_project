using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;

namespace PLProject.Controllers
{
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Pharmacist}")]
    public class MedicationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;

        public MedicationController(IUnitOfWork unitOfWork , IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.env = env;
        }

        public IActionResult Index(string searchQuery, int? page)
        {
            var medications = unitOfWork.Repository<Medication>().GetALL().ToList();
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
                ActSubDateReader = unitOfWork.Repository<ActiveSubstance>().GetALL().ToList(), // Load active substances
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
                unitOfWork.Repository<Medication>().Add(medication);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            medicationViewModel.ActSubDateReader = unitOfWork.Repository<ActiveSubstance>().GetALL().ToList(); // Reload active substances
            return View(medicationViewModel);
        }

        public IActionResult Details(int id)
        {
            var spec = new BaseSpecification<Medication>(e => e.Id == id);
            spec.Includes.Add(e => e.PrescriptionItemMedications);
            
            spec.Includes.Add(e => e.ActiveSubstances);
            
            var medication = unitOfWork.Repository<Medication>().GetEntityWithSpec(spec);
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
            var medication = unitOfWork.Repository<Medication>().Get(id);
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
                unitOfWork.Repository<Medication>().Update(medication);
                unitOfWork.Complete();
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
            var medication = unitOfWork.Repository<Medication>().Get(id);
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
            var medication = unitOfWork.Repository<Medication>().Get(id);
            if (medication == null)
                return NotFound();

            unitOfWork.Repository<Medication>().Delete(medication);
            unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

    }
}
