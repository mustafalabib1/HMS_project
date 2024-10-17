using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using PLProject.ViewModels.AppointmentViewModel;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Patient)]
	public class AppointmentPatientController : Controller
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public AppointmentPatientController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
		{
			this.unitOfWork = unitOfWork;
			env = _env;
		}

		#region Get all Appointment for patient 
		public IActionResult Index(int? page, int PatientId = 1)
		{

			var appointments = unitOfWork.Repository<Apointment>().Find(a => a.PatientId == PatientId).Include(a => a.Patient).Include(a => a.Doctor).Include(a => a.Clinic).ToList();
			var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
			// Pagination logic
			int pageSize = 10;
			int pageNumber = page ?? 1;

			var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

			return View(paginatedList);
		}
		#endregion

		#region Patient Create Appointment
		public IActionResult Create(ClinicAvailabilityViewModel model)
		{
			// Set the default year and month to the current if not provided
			DateTime today = DateTime.Now;
			if (model.SelectedYear == 0) model.SelectedYear = today.Year;
			if (model.SelectedMonth == 0) model.SelectedMonth = today.Month;

			// If clinicId is provided, fetch available appointments
			if (model.SelectedClinicId.HasValue)
			{
				var doctors = unitOfWork.Repository<Doctor>().Find(d => d.ClinicId == model.SelectedClinicId.Value).ToList();

				var availableDays = unitOfWork.Repository<DoctorScheduleLookup>().Find(ds => doctors.Select(d => d.Id).Contains(ds.Id)).GroupBy(ds => ds.Day);

				int daysInCurrentMonth = DateTime.DaysInMonth(model.SelectedYear, model.SelectedMonth);

				List<DayAvailability> availabilityList = new List<DayAvailability>();

				// Generate availability data for each day in the selected month
				for (int i = 0; i < daysInCurrentMonth; i++)
				{
					DayAvailability day = new DayAvailability();
					day.Date = new DateTime(model.SelectedYear, model.SelectedMonth, i + 1);

					// Check if the date is available
					if (day.Date <= today)
					{
						day.IsAvailable = false;
						day.AvailableDoctors = null;
					}
					else
					{
						foreach (var avaday in availableDays)
						{
							if (avaday.Key == day.Date.DayOfWeek)
								foreach (var item in avaday)
								{
									var count = unitOfWork.Repository<Apointment>()
										.Find(a => a.ApointmentDate == DateOnly.FromDateTime(day.Date)
											  && a.ApointmentTime < item.StartTime
											  && a.ApointmentTime < item.EndTime
											  && a.ApointmentStatus!=ApointmentStatusEnum.Cancelled).Count();
									if (count < 50)
									{
										day.IsAvailable = true;
										if (day.AvailableDoctors == null)
										{
											day.AvailableDoctors = new List<DoctorScheduleLookup>();
										}
										day.AvailableDoctors.Add(item);
										unitOfWork.Complete();

									}
								}
						}
					}

					availabilityList.Add(day);
					unitOfWork.Complete();
				}

				model.AvailableDays = availabilityList;
			}

			// Set the selected year and month in the model
			model.SelectedYear = model.SelectedYear;
			model.SelectedMonth = model.SelectedMonth;
			model.SelectedClinicId = model.SelectedClinicId;

			return View(model);
		}

		[HttpPost]
		public IActionResult ConfirmAppointment(ApointmentCreateVM model)
		{
			model.PatientId = 1;
			if (ModelState.IsValid)
			{
				unitOfWork.Repository<Apointment>().Add(new Apointment().ConvertApointmentCreateVMToApointment(model));
				unitOfWork.Complete();

				TempData["SuccessMessage"] = "Appointment booked successfully!";
				return RedirectToAction("Index");
			}
			// If no date was selected or there was an issue
			TempData["ErrorMessage"] = "Please select a valid date and time for booking.";
			return RedirectToAction("Create", new { clinicId = model.ClinicId });
		}
		#endregion

		#region Details

		public IActionResult Details(int? Id, string viewname = "Details")
		{
			if (!Id.HasValue)
				return BadRequest(); // 400
			var spec = new BaseSpecification<Apointment>(a => a.Id == Id);
			spec.Includes.Add(a => a.Patient);
			spec.Includes.Add(a => a.Doctor);
			spec.Includes.Add(a => a.Clinic);
			spec.Includes.Add(a => a.Prescription);
			var apointment = unitOfWork.Repository<Apointment>().GetEntityWithSpec(spec);
			var apointmentVM = apointment.ConvertApointmentToAppointmentGenarelVM();

			if (apointmentVM is null)
				return NotFound(); // 404

			return View(viewname, apointmentVM);
		}
		#endregion

		#region Delete
		public IActionResult Delete(int? Id)
		{
			return Details(Id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete([FromRoute]int Id ,AppointmentGenarelVM ViewModel)
		{
			if (Id != ViewModel.Id)
				return BadRequest();
			try
			{
				var apointment = unitOfWork.Repository<Apointment>().Get(ViewModel.Id);
				apointment.ApointmentStatus = ApointmentStatusEnum.Cancelled;

				unitOfWork.Repository<Apointment>().Update(apointment);
				unitOfWork.Complete();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{

				if (env.IsDevelopment())
					ModelState.AddModelError(string.Empty, ex.Message);
				else
					ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");

				return View(ViewModel);
			}
		}
		#endregion
	}
}





