using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PLProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IRepository<Patient> PatientRepo;

        public PatientController(IRepository<Patient> PatientRepo)
        {
            this.PatientRepo = PatientRepo;
        }


        public IActionResult Index()
        {
            var Patients = PatientRepo.GetALL();
            var PatientViewModels = Patients.Select(p => (PatientViewModel)p).ToList();
            return View(PatientViewModels);
        }


        #region Create
        public IActionResult Create()
        {
            return View(new PatientViewModel());
        }

        [HttpPost]
        public IActionResult Create(PatientViewModel PatientViewModel)
        {
            if (ModelState.IsValid) // server side validation
            {
                PatientRepo.Add((Patient)PatientViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(PatientViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Patient = PatientRepo.Get(Id.Value);
            var PatientViewModel = (PatientViewModel)Patient;

            if (Patient is null)
                return NotFound(); // 404

            return View(PatientViewModel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Patient = PatientRepo.Get(Id.Value);

            if (Patient is null)
                return NotFound(); // 404

            var PatientViewModel = (PatientViewModel)Patient;
            return View(PatientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel PatientViewModel)
        {
            var Patient = PatientRepo.Get(PatientViewModel.Id);
            PatientViewModel.UserPassword = Patient.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                PatientRepo.Update((Patient)PatientViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(PatientViewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Patient = PatientRepo.Get(Id.Value);
            var PatientViewModel = (PatientViewModel)Patient;

            if (Patient is null)
                return NotFound(); // 404

            return View(PatientViewModel);
        }

        [HttpPost]
        public IActionResult Delete(PatientViewModel PatientViewModel)
        {
            var Patient = PatientRepo.Get(PatientViewModel.Id);
            try
            {
                PatientRepo.Delete(Patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(PatientViewModel);
            }
        }
        #endregion
    }
}
