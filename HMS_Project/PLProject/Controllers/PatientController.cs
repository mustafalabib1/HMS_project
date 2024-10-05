using BLLProject.Interfaces;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PLProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IRepository<Patient> patientRepo;

        public IRepository<Patient> SpecializationRepo { get; }

        public PatientController(IRepository<Patient> SpecializationRepo, IRepository<Patient> PatientRepo)
        {
            this.SpecializationRepo = SpecializationRepo;
            patientRepo = PatientRepo;
        }
        public IActionResult Index()
        {
            var patients = patientRepo.GetALL();
            var patientViewModels = patients.Select(p => (Patient)p).ToList();
            return View(patientViewModels);
        }

        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid) // server side validation
            {
                var count = patientRepo.Add((patient));
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var patient = patientRepo.Get(Id.Value);
            var patientViewModel = (Patient)patient;

            if (patient is null)
                return NotFound(); // 404

            return View(patientViewModel);
        }

        #endregion

        #region Delete

        #endregion

    }
}
