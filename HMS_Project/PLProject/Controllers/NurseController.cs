using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Numerics;

namespace PLProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class NurseController : Controller
    {
        #region DPI
        private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public NurseController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
        {
            this.unitOfWork = unitOfWork;
			env = _env;
		}
        #endregion

        #region Index 
        public IActionResult Index()
        {
            var Nurses = unitOfWork.Repository<Nurse>().GetALL();
            var NurseViewModels = Nurses.Select(p => (NurseViewModel)p).ToList();
            return View(NurseViewModels);
        } 
        #endregion

        #region Details
        [Route("Nurse/Details/{userId}")]
        public IActionResult Details(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var Nurse = unitOfWork.Repository<Nurse>().Get(userId);

            if (Nurse is null)
                return NotFound(); // 404

            var NurseViewModel = (NurseViewModel)Nurse;

            return View(NurseViewModel);
        }
        #endregion

        #region Edit
        [Route("Nurse/Edit/{userId}")]
        public IActionResult Edit(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var Nurse = unitOfWork.Repository<Nurse>().Get(userId);

            if (Nurse is null)
                return NotFound(); // 404

            var NurseViewModel = (NurseViewModel)Nurse;
            return View(NurseViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Nurse/Edit/{userId}")]
        public IActionResult Edit([FromRoute] string userId, NurseViewModel ViewModel)
        {

            if (userId != ViewModel.UserId)
                return BadRequest();//400
            var nurse = unitOfWork.Repository<Nurse>().Get(ViewModel.UserId);
			if (ModelState.IsValid)
			{
				try
				{
					nurse.UpdateInfo(ViewModel);
					//unitOfWork.Repository<Doctor>().Update(doctor);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "nurse update successfully!";

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during update nurse";
					return View(ViewModel);
				}
			}

			return View(ViewModel);
		}
        #endregion
    }
}
