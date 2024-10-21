using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PLProject.Controllers
{
    public class PatientController : Controller
    {
        #region DPi
        private readonly IUnitOfWork unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region Idnex
        public IActionResult Index()
        {
            var Patients = unitOfWork.Repository<Patient>().GetALL();
            var PatientViewModels = Patients.Select(p => (PatientViewModel)p).ToList();
            return View(PatientViewModels);
        } 
        #endregion

        #region Details
        public IActionResult Details(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var Patient = unitOfWork.Repository<Patient>().Get(userId);

            if (Patient is null)
                return NotFound(); // 404

            var PatientViewModel = (PatientViewModel)Patient;

            return View(PatientViewModel);
        }
        #endregion

        #region Edit
        [Route("Patient/Edit/{userId}")]
        public IActionResult Edit(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var Patient = unitOfWork.Repository<Patient>().Get(userId);

            if (Patient is null)
                return NotFound(); // 404

            var PatientViewModel = (PatientViewModel)Patient;
            return View(PatientViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Patient/Edit/{userId}")]
        public IActionResult Edit([FromRoute] int Id, PatientViewModel ViewModel)
        {
            if (Id != ViewModel.Id)
                return BadRequest();//400
            var patient = unitOfWork.Repository<Patient>().Get(ViewModel.UserId);
            if (ModelState.IsValid) // server side validation
            {
                patient.UpdateInfo(ViewModel);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ViewModel);
        }
        #endregion
    }
}
