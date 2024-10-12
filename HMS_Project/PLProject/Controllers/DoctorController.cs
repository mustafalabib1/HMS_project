using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PLProject.Helpers;
using BLLProject.Specification;

namespace PLProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class DoctorController : Controller
	{
        private readonly IUnitOfWork unitOfWork;

        public DoctorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Index (List Doctors)
        public IActionResult Index()
        {
            var doctors = unitOfWork.Repository<Doctor>().GetALL();
            var doctorViewModels = doctors.Select(d => (DoctorViewModel)d).ToList();
            return View(doctorViewModels);
        }
        #endregion

        #region Specialization
        public IActionResult AddSpecialization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSpecialization(DoctorSpecializationLookup doctorSpecializationLookup)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Repository<DoctorSpecializationLookup>().Add(doctorSpecializationLookup);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorSpecializationLookup);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            var ViewModel = new DoctorViewModel() { SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL() };
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Repository<Doctor>().Add((Doctor)doctorViewModel);
                return RedirectToAction(nameof(Index));
            }
            doctorViewModel.SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL();
            return View(doctorViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var spec = new BaseSpecification<Doctor>(e => e.Id == Id);
            spec.Includes.Add(e => e.DoctorSpecialization);

            var doctor = unitOfWork.Repository<Doctor>().GetEntityWithSpec(spec);
            var doctorViewModel = (DoctorViewModel)doctor;

            if (doctor is null)
                return NotFound(); // 404

            return View(doctorViewModel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var doctor = unitOfWork.Repository<Doctor>().Get(Id.Value);

            if (doctor is null)
                return NotFound(); // 404

            var doctorViewModel = (DoctorViewModel)doctor;
            doctorViewModel.SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL();
            return View(doctorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel doctorViewModel)
        {
            Doctor doctor = unitOfWork.Repository<Doctor>().Get(doctorViewModel.Id);
            doctorViewModel.UserPassword=doctor.UserPassword;
            if (ModelState.IsValid)
            {
                unitOfWork.Repository<Doctor>().Update((Doctor)doctorViewModel);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            doctorViewModel.SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL();
            return View(doctorViewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var doctor = unitOfWork.Repository<Doctor>().Get(Id.Value);
            var doctorViewModel = (DoctorViewModel)doctor;

            if (doctor is null)
                return NotFound(); // 404

            return View(doctorViewModel);
        }
        [HttpPost]
        public IActionResult Delete(DoctorViewModel doctorViewModel)
        {
            var doctor = unitOfWork.Repository<Doctor>().Get(doctorViewModel.Id);
            try
            {
                unitOfWork.Repository<Doctor>().Delete(doctor);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(doctor);
            }
        }
        #endregion
    }
}
