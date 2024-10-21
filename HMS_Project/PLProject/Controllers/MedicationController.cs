using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace PLProject.Controllers
{
    [Authorize(Roles = $"{Roles.Admin}, {Roles.Pharmacist}")]
    public class MedicationController : Controller
    {
        #region DPI
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;

        public MedicationController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.env = env;
        }

        #endregion

        #region Index 
        // Index Action
        public IActionResult Index(string searchQuery, int page = 1)
        {
            var medications = unitOfWork.Repository<Medication>().GetALL();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                medications = medications.Where(m => m.MedName.Contains(searchQuery)).ToList();
            }

            var medicationViewModels = medications.Select(m => new MedicationViewModel
            {
                Id = m.Id,
                Name = m.MedName,
                Strength = m.Strength,
            }).ToList();

            var pagedList = medicationViewModels.ToPagedList(page, 10);
            ViewData["CurrentFilter"] = searchQuery;

            return View(pagedList);
        }
        #endregion

        #region Greate
        // Create Action (GET)
        public IActionResult Create()
        {
            var activeSubstances = unitOfWork.Repository<ActiveSubstance>().GetALL().Select(a => new ActiveSubstanceViewModel
            {
                Id = a.Id,
                ActiveSubstancesName = a.ActiveSubstancesName
            }).ToList();

            var viewModel = new MedicationViewModel
            {
                ActiveSubstances = activeSubstances,
                ActiveSubstanceIds = new List<int>() // Initialize the list for selected IDs
            };

            return View(viewModel);
        }

        // POST: Medication/Create
        [HttpPost]
        public IActionResult Create(MedicationViewModel medicationViewModel)
        {
            if (ModelState.IsValid)
            {
                var medication = new Medication
                {
                    MedName = medicationViewModel.Name,
                    Strength = medicationViewModel.Strength,
                    ActiveSubstances = new List<ActiveSubstance>()
                };

                // Collect selected active substances based on ActiveSubstanceIds
                foreach (var id in medicationViewModel.ActiveSubstanceIds)
                {
                    var activeSubstance = unitOfWork.Repository<ActiveSubstance>().GetALL().FirstOrDefault(a => a.Id == id);
                    if (activeSubstance != null)
                    {
                        medication.ActiveSubstances.Add(activeSubstance);
                    }
                }

                // Add the medication to the repository
                unitOfWork.Repository<Medication>().Add(medication);
                unitOfWork.Complete(); // Save changes
                return RedirectToAction(nameof(Index)); // Redirect to the index action after successful creation
            }

            // If model state is invalid, repopulate the active substances
            medicationViewModel.ActiveSubstances = unitOfWork.Repository<ActiveSubstance>().GetALL().Select(a => new ActiveSubstanceViewModel
            {
                Id = a.Id,
                ActiveSubstancesName = a.ActiveSubstancesName
            }).ToList();

            return View(medicationViewModel); // Return the view with the model to show validation errors
        }
        #endregion

        #region Edit
        // Edit Action (GET)
        public IActionResult Edit(int id)
        {
            var medication = unitOfWork.Repository<Medication>().Get(id);
            if (medication == null) return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                Id = medication.Id,
                Name = medication.MedName,
                Strength = medication.Strength,
                ActiveSubstanceIds = medication.ActiveSubstances.Select(a => a.Id).ToList(),
                ActiveSubstances = unitOfWork.Repository<ActiveSubstance>().GetALL().Select(a => new ActiveSubstanceViewModel
                {
                    Id = a.Id,
                    ActiveSubstancesName = a.ActiveSubstancesName
                }).ToList()
            };

            return View(medicationViewModel);
        }

        // Edit Action (POST)
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int Id, MedicationViewModel ViewModel)
        {

            if (Id != ViewModel.Id)
                return BadRequest();//400
            if (ModelState.IsValid)
            {
                // Retrieve the existing medication from the database
                var medication = unitOfWork.Repository<Medication>().Get(ViewModel.Id);

                if (medication == null) return NotFound();

                // Update medication properties
                medication.MedName = ViewModel.Name;
                medication.Strength = ViewModel.Strength;

                // Update the active substances
                medication.ActiveSubstances = ViewModel.ActiveSubstanceIds
                    .Select(id => new ActiveSubstance { Id = id })
                    .ToList();

                // Save changes to the repository
                unitOfWork.Repository<Medication>().Update(medication);

                unitOfWork.Complete(); // Commit the changes to the database

                return RedirectToAction(nameof(Index)); // Redirect to the index action after successful update
            }

            // If the model state is invalid, repopulate the active substances
            ViewModel.ActiveSubstances = unitOfWork.Repository<ActiveSubstance>().GetALL().Select(a => new ActiveSubstanceViewModel
            {
                Id = a.Id,
                ActiveSubstancesName = a.ActiveSubstancesName
            }).ToList();

            return View(ViewModel); // Return the view with the model to show validation errors
        }
        #endregion

        #region Details
        // Details Action
        public IActionResult Details(int id)
        {
            var medication = unitOfWork.Repository<Medication>().Get(id);
            if (medication == null) return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                Id = medication.Id,
                Name = medication.MedName,
                Strength = medication.Strength,
                ActiveSubstances = medication.ActiveSubstances.Select(a => new ActiveSubstanceViewModel
                {
                    Id = a.Id,
                    ActiveSubstancesName = a.ActiveSubstancesName,
                }).ToList()
            };

            return View(medicationViewModel);
        } 
        #endregion

        public IActionResult Delete(int id)
        {
            var medication = unitOfWork.Repository<Medication>().Get(id);
            if (medication == null) return NotFound();

            var medicationViewModel = new MedicationViewModel
            {
                Id = medication.Id,
                Name = medication.MedName,
                Strength = medication.Strength,
                ActiveSubstances = medication.ActiveSubstances.Select(a => new ActiveSubstanceViewModel
                {
                    Id = a.Id,
                    ActiveSubstancesName = a.ActiveSubstancesName,
                }).ToList()
            };

            return View(medicationViewModel);
        }

        // Delete Action (POST)
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, MedicationViewModel ViewModel)
        {

            if (Id != ViewModel.Id)
                return BadRequest();//400
            try
            {
                var medication = unitOfWork.Repository<Medication>().Get(ViewModel.Id);
                unitOfWork.Repository<Medication>().Delete(medication);
                unitOfWork.Complete();


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");

                return View(ViewModel);
            }
        }
    }
}