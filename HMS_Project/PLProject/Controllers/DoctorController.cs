using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.ViewModels;

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
				unitOfWork.Complete();
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
				unitOfWork.Complete();
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

            doctorViewModel.UserPassword = doctor.UserPassword;
			ModelState.Remove("UserPassword");
            if (ModelState.IsValid)
			{
				doctor.UpdatedDoctor(doctorViewModel);
				unitOfWork.Repository<Doctor>().Update(doctor);
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
		#region Create schedule
		IActionResult CreateSchedule(int DocotorId)
		{
			return View();
		}
		[HttpPost]
		IActionResult CreateSchedule (DoctorScheduleLookup Seschedule )
		{
			return View();
		}
        #endregion
        #region Delete Schedule day 
        [HttpPost]
        public IActionResult DeleteScheduleDay(int ScheduleId)
        {
            try
            {
				// Find the schedule entry based on DoctorId and DayId
				var scheduleDay = unitOfWork.Repository<DoctorScheduleLookup>().Get(ScheduleId);

                if (scheduleDay == null)
                {
                    return Json(new { success = false, message = "Schedule day not found." });
                }

                // Delete the schedule day
                unitOfWork.Repository<DoctorScheduleLookup>().Delete(scheduleDay);
                unitOfWork.Complete();

                // Return success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Return error response
                return Json(new { success = false, message = "Error occurred: " + ex.Message });
            }
        }
        #endregion
    }
}
