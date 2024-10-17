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

        #region Details
        [Route("Doctor/Details/{userId}")]
        public IActionResult Details(string userId)
		{
			if (userId is null)
				return BadRequest(); // 400

			var spec = new BaseSpecification<Doctor>(e => e.UserId == userId);
			spec.Includes.Add(e => e.DoctorSpecialization);

			var doctor = unitOfWork.Repository<Doctor>().GetEntityWithSpec(spec);

			if (doctor is null)
				return NotFound(); // 404

			var doctorViewModel = (DoctorViewModel)doctor;

			return View(doctorViewModel);
		}
        #endregion

        #region Edit
        [HttpGet]
        [Route("Doctor/Edit/{userId}")]
        public IActionResult Edit(string userId)
		{
			if (userId is null)
                return BadRequest(); // 400

			var doctor = unitOfWork.Repository<Doctor>().Get(userId);
			unitOfWork.Complete();

			if (doctor is null)
				return NotFound(); // 404

			var doctorViewModel = (DoctorViewModel)doctor;

			doctorViewModel.SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL();
            unitOfWork.Complete();

            return View(doctorViewModel);
		}

		[HttpPost]
        [Route("Doctor/Edit/{userId}")]
        public IActionResult Edit(DoctorViewModel doctorViewModel)
		{
			Doctor doctor = unitOfWork.Repository<Doctor>().Get(doctorViewModel.UserId);
			
			ModelState.Remove<DoctorViewModel>(d => d.schedule);
            if (ModelState.IsValid)
			{
                doctor.UpdatedDoctor(doctorViewModel);
                unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			doctorViewModel.SpecializationsDateReader = unitOfWork.Repository<DoctorSpecializationLookup>().GetALL();
			return View(doctorViewModel);
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
