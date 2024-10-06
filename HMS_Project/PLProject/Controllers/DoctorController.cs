using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
	public class DoctorController : Controller
	{
          private readonly IRepository<Doctor> doctorRepo;
        public IRepository<DoctorSpecializationLookup> SpecializationRepo { get; }

        public DoctorController(IRepository<DoctorSpecializationLookup> SpecializationRepo, IRepository<Doctor> DoctorRepo)
        {
            this.SpecializationRepo = SpecializationRepo;
            doctorRepo = DoctorRepo;
        }

        #region Index (List Doctors)
        public IActionResult Index()
        {
            var doctors = doctorRepo.GetALL();
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
                SpecializationRepo.Add(doctorSpecializationLookup);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorSpecializationLookup);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            var ViewModel = new DoctorViewModel() { SpecializationsDateReader = SpecializationRepo.GetALL() };
            return View(ViewModel);
        }

        [HttpPost]
        public IActionResult Create(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                doctorRepo.Add((Doctor)doctorViewModel);
                return RedirectToAction(nameof(Index));
            }
            doctorViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(doctorViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var doctor = doctorRepo.Get(Id.Value);
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

            var doctor = doctorRepo.Get(Id.Value);

            if (doctor is null)
                return NotFound(); // 404

            var doctorViewModel = (DoctorViewModel)doctor;
            doctorViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(doctorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DoctorViewModel doctorViewModel)
        {
            Doctor doctor = doctorRepo.Get(doctorViewModel.Id);

            if (ModelState.IsValid)
            {
                doctor.FullName = doctorViewModel.FirstName;
                doctor.SpecializationId = doctorViewModel.specializationId??0;
                doctor.Phone = doctorViewModel.Phone;

                doctorRepo.Update(doctor);
                return RedirectToAction(nameof(Index));
            }
            doctorViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
            return View(doctorViewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var doctor = doctorRepo.Get(Id.Value);
            var doctorViewModel = (DoctorViewModel)doctor;

            if (doctor is null)
                return NotFound(); // 404

            return View(doctorViewModel);
        }
        [HttpPost]
        public IActionResult Delete(DoctorViewModel doctorViewModel)
        {
            var doctor = doctorRepo.Get(doctorViewModel.Id);
            try
            {
                doctorRepo.Delete(doctor);
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
