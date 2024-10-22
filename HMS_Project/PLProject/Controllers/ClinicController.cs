using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;

namespace PLProject.Controllers
{
	[Authorize(Roles = Roles.Admin)]
	public class ClinicController : Controller
	{
		#region DPI
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public ClinicController(IUnitOfWork unitOfWork, IWebHostEnvironment Env)
		{
			this.unitOfWork = unitOfWork;
			env = Env;
		}
		#endregion

		#region Index
		public IActionResult Index()
		{
			var clinics = unitOfWork.Repository<Clinic>().GetALL();
			var clinicViewModels = clinics.Select(c => (ClinicViewModel)c).ToList();
			return View(clinicViewModels);
		}
		#endregion

		#region Specialization
		public IActionResult AddSpecialization()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddSpecialization(ClinicSpecializationLookup ClinicSpecialization)
		{
			if (ModelState.IsValid) // server side validation
			{
				unitOfWork.Repository<ClinicSpecializationLookup>().Add(ClinicSpecialization);
				unitOfWork.Complete();
				return RedirectToAction(nameof(Index));
			}
			return View(ClinicSpecialization);
		}
		#endregion

		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(ClinicViewModel clinicViewModel)
		{
			if (ModelState.IsValid) // server side validation
			{
				return RedirectToAction(nameof(Index));
				try
				{
					var updatedClinic = (Clinic)clinicViewModel;

					var AddedDoctor = unitOfWork.Repository<Doctor>().Find(d => clinicViewModel.DoctorsAddedId.Contains(d.Id));
					var AddedNurse = unitOfWork.Repository<Nurse>().Find(d => clinicViewModel.NursesAddedId.Contains(d.Id));

					if (AddedDoctor is not null)
					{
						clinicViewModel.Doctors.AddRange(AddedDoctor);
						updatedClinic.Doctors = clinicViewModel.Doctors;
					}

					if (AddedNurse is not null)
					{
						clinicViewModel.Nurses.AddRange(AddedNurse);
						updatedClinic.Nurses = clinicViewModel.Nurses;
					}
					unitOfWork.Repository<Clinic>().Add(updatedClinic);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "Clinic Create successfully!";


					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					// Handle exceptions and add error messages to the model state

					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during the Create.";

					return View(clinicViewModel);
				}

			}
			return View(clinicViewModel);
		}
		#endregion

		#region Details
		public IActionResult Details(int? Id)
		{
			if (!Id.HasValue)
				return BadRequest(); // 400

			var spec = new BaseSpecification<Clinic>(c => c.Id == Id);
			spec.Includes.Add(c => c.ClinicSpecilization);
			unitOfWork.Complete();

			var clinic = unitOfWork.Repository<Clinic>().GetEntityWithSpec(spec);
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

			return View(clinicViewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute] int Id, ClinicViewModel viewModel)
		{
			if (Id != viewModel.Id)
				return BadRequest();//400

			Clinic updatedClinic = unitOfWork.Repository<Clinic>().Get(viewModel.Id);

			if (ModelState.IsValid) // server side validation
			{
				try
				{
					// Mapping the model
					updatedClinic.Name = viewModel.Name;
					updatedClinic.Specialization = viewModel.Specialization;
					updatedClinic.Phone = viewModel.Phone;
					updatedClinic.Price = viewModel.Price ?? default;

					viewModel.Doctors = updatedClinic.Doctors.ToList();
					viewModel.Nurses = updatedClinic.Nurses.ToList();

					var AddedDoctor = unitOfWork.Repository<Doctor>().Find(d => viewModel.DoctorsAddedId.Contains(d.Id));
					var AddedNurse = unitOfWork.Repository<Nurse>().Find(d => viewModel.NursesAddedId.Contains(d.Id));

					if (AddedDoctor is not null)
					{
						viewModel.Doctors.AddRange(AddedDoctor);
						updatedClinic.Doctors = viewModel.Doctors;
					}

					if (AddedNurse is not null)
					{
						viewModel.Nurses.AddRange(AddedNurse);
						updatedClinic.Nurses = viewModel.Nurses;
					}

					unitOfWork.Repository<Clinic>().Update(updatedClinic);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "clinic update successfully!";


					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					// Handle exceptions and add error messages to the model state

					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during the update.";

					return View(viewModel);
				}

				return RedirectToAction(nameof(Index));
			}
			return View(viewModel);
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

			if (Id != clinicViewModel.Id)
			{
				try
				{
					var clinic = unitOfWork.Repository<Clinic>().Get(clinicViewModel.Id);
					unitOfWork.Repository<Clinic>().Delete(clinic);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "clinic delete successfully!";

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					// Handle exceptions and add error messages to the model state

					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during the delete.";

					return View(clinicViewModel);
				}
			}
			return View(clinicViewModel);

		}
		#endregion

		#region RemoveDoctor
		[HttpPost]
		public IActionResult RemoveDoctor(int doctorId, int clinicId)
		{
			try
			{
				// Find the schedule entry based on DoctorId and DayId
				var doctor = unitOfWork.Repository<Doctor>().Find(d => d.Id == doctorId).FirstOrDefault();
				var clinic = unitOfWork.Repository<Clinic>().Get(clinicId);

				if (doctor == null)
				{
					return Json(new { success = false, message = "Doctor not found." });
				}

				// Delete the doctor from clinic 
				clinic.Doctors.Remove(doctor);
				unitOfWork.Repository<Clinic>().Update(clinic);
				unitOfWork.Complete();

				// Return success response
				return Ok();
			}
			catch (Exception ex)
			{
				// Return error response
				return Json(new { success = false, message = "Error occurred: " + ex.Message });
			}
		}
		#endregion

		#region RemoveNurse
		[HttpPost]
		public IActionResult RemoveNurse(int nurseId, int clinicId)
		{
			try
			{
				// Find the clinic and nurse 
				var nurse = unitOfWork.Repository<Nurse>().Get(nurseId);
				var clinic = unitOfWork.Repository<Clinic>().Get(clinicId);


				if (nurse == null)
				{
					return Json(new { success = false, message = "Nurse not found." });
				}

				// Delete the nurse from clinic 
				clinic.Nurses.Remove(nurse);
				unitOfWork.Repository<Clinic>().Update(clinic);
				unitOfWork.Complete();


				// Return success response
				return Ok();
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
