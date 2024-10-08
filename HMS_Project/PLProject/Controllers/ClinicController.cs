using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class ClinicController : Controller
    {
        #region DPI
        private readonly IRepository<Clinic> clinicRepo;

        public IRepository<ClinicSpecializationLookup> SpecializationRepo { get; }

        public ClinicController(IRepository<ClinicSpecializationLookup> SpecializationRepo, IRepository<Clinic> ClinicRepo)
        {
            this.SpecializationRepo = SpecializationRepo;
            clinicRepo = ClinicRepo;
        }
        #endregion
        public IActionResult Index()
        {
            var clinics = clinicRepo.GetALL();
            var clinicViewModels = clinics.Select(c => (ClinicViewModel)c).ToList();
            return View(clinicViewModels);
        }

        #region Specialization
        public IActionResult AddSpecialization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSpecialization(ClinicSpecializationLookup clinicSpecializationLookup)
        {
            if (ModelState.IsValid) // server side validation
            {
                SpecializationRepo.Add(clinicSpecializationLookup);
                return RedirectToAction(nameof(Index));
            }
            return View(clinicSpecializationLookup);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            var ViewModel = new ClinicViewModel() { SpecializationsDateReader = SpecializationRepo.GetALL() };

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Create(ClinicViewModel clinicViewModel)
        {
            if (ModelState.IsValid) // server side validation
            {
                clinicRepo.Add((Clinic)clinicViewModel);
                return RedirectToAction(nameof(Index));
            }
            clinicViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(clinicViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var clinic = clinicRepo.Get(Id.Value);
            var clinicViewModel = (ClinicViewModel)clinic;

            if (clinic is null)
                return NotFound(); // 404

            return View(clinicViewModel);
        }

        #endregion

        #region Edit

        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var clinic = clinicRepo.Get(Id.Value);

            if (clinic is null)
                return NotFound(); // 404

            var clinicViewModel = (ClinicViewModel)clinic;

            clinicViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(clinicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int Id, ClinicViewModel clinicViewModel)
        {
            Clinic clinic = clinicRepo.Get(clinicViewModel.Id);

            if (ModelState.IsValid) // server side validation
            {
                // Mapping the model
                clinic.Name = clinicViewModel.Name;
                clinic.Specialization = clinicViewModel.Specialization;
                clinic.Phone = clinicViewModel.Phone;
                clinic.Price = clinicViewModel.Price ?? default;

                clinicRepo.Update(clinic);
                return RedirectToAction(nameof(Index));
            }
            clinicViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(clinicViewModel);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var clinic = clinicRepo.Get(Id.Value);
            var clinicViewModel = (ClinicViewModel)clinic;

            if (clinic is null)
                return NotFound(); // 404

            return View(clinicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, ClinicViewModel clinicViewModel)
        {
            var clinic = clinicRepo.Get(clinicViewModel.Id);
            try
            {
                clinicRepo.Delete(clinic);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(clinic);
            }
        }
        #endregion

    }
}
