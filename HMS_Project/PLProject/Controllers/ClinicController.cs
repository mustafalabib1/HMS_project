using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PLProject.Helpers;

namespace PLProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ClinicController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        #region DPI


        public ClinicController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public IActionResult Index()
        {
            var clinics = unitOfWork.Repository<Clinic>().GetALL();
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
                unitOfWork.Repository<ClinicSpecializationLookup>().Add(clinicSpecializationLookup);
                return RedirectToAction(nameof(Index));
            }
            return View(clinicSpecializationLookup);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            var ViewModel = new ClinicViewModel() { SpecializationsDateReader = unitOfWork.Repository<ClinicSpecializationLookup>().GetALL() };

            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Create(ClinicViewModel clinicViewModel)
        {
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Clinic>().Add((Clinic)clinicViewModel);
                return RedirectToAction(nameof(Index));
            }
            clinicViewModel.SpecializationsDateReader = unitOfWork.Repository<ClinicSpecializationLookup>().GetALL();
            return View(clinicViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var clinic = unitOfWork.Repository<Clinic>().Get(Id.Value);
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

            var clinic = unitOfWork.Repository<Clinic>().Get(Id.Value);

            if (clinic is null)
                return NotFound(); // 404

            var clinicViewModel = (ClinicViewModel)clinic;

            clinicViewModel.SpecializationsDateReader = unitOfWork.Repository<ClinicSpecializationLookup>().GetALL();
            return View(clinicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int Id, ClinicViewModel clinicViewModel)
        {
            Clinic clinic = unitOfWork.Repository<Clinic>().Get(clinicViewModel.Id);

            if (ModelState.IsValid) // server side validation
            {
                // Mapping the model
                clinic.Name = clinicViewModel.Name;
                clinic.Specialization = clinicViewModel.Specialization;
                clinic.Phone = clinicViewModel.Phone;
                clinic.Price = clinicViewModel.Price ?? default;

                unitOfWork.Repository<Clinic>().Update(clinic);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            clinicViewModel.SpecializationsDateReader = unitOfWork.Repository<ClinicSpecializationLookup>().GetALL();
            return View(clinicViewModel);
        }

        #endregion

        #region Delete

        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var clinic = unitOfWork.Repository<Clinic>().Get(Id.Value);
            var clinicViewModel = (ClinicViewModel)clinic;

            if (clinic is null)
                return NotFound(); // 404

            return View(clinicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int Id, ClinicViewModel clinicViewModel)
        {
            var clinic = unitOfWork.Repository<Clinic>().Get(clinicViewModel.Id);
            try
            {
                unitOfWork.Repository<Clinic>().Delete(clinic);
                unitOfWork.Complete();
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
