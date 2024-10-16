using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PLProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var Patients = unitOfWork.Repository<Patient>().GetALL();
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
                unitOfWork.Repository<Patient>().Add((Patient)PatientViewModel);
                unitOfWork.Complete();
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

            var Patient = unitOfWork.Repository<Patient>().Get(Id.Value);
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

            var Patient = unitOfWork.Repository<Patient>().Get(Id.Value);

            if (Patient is null)
                return NotFound(); // 404

            var PatientViewModel = (PatientViewModel)Patient;
            return View(PatientViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PatientViewModel PatientViewModel)
        {
            var Patient = unitOfWork.Repository<Patient>().Get(PatientViewModel.Id);
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Patient>().Update((Patient)PatientViewModel);
                unitOfWork.Complete();
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

            var Patient = unitOfWork.Repository<Patient>().Get(Id.Value);
            var PatientViewModel = (PatientViewModel)Patient;

            if (Patient is null)
                return NotFound(); // 404

            return View(PatientViewModel);
        }

        [HttpPost]
        public IActionResult Delete(PatientViewModel PatientViewModel)
        {
            var Patient = unitOfWork.Repository<Patient>().Get(PatientViewModel.Id);
            try
            {
                unitOfWork.Repository<Patient>().Delete(Patient);
                unitOfWork.Complete();
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
