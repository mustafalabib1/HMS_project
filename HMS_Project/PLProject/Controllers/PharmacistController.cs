using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PLProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class PharmacistController : Controller
    {
        #region DPI
        private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment env;

		public PharmacistController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
        {
            this.unitOfWork = unitOfWork;
			env = _env;
		}
        #endregion

        #region Index 
        public IActionResult Index()
        {
            var pharmacists = unitOfWork.Repository<Pharmacist>().GetALL();
            var pharmacistViewModels = pharmacists.Select(p => (PharmacistViewModel)p).ToList();
            return View(pharmacistViewModels);
        } 
        #endregion

        #region Details
        [Route("Pharmacist/Details/{userId}")]
        public IActionResult Details(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(userId);

            if (pharmacist is null)
                return NotFound(); // 404

            var pharmacistViewModel = (PharmacistViewModel)pharmacist;

            return View(pharmacistViewModel);
        }
        #endregion

        #region Edit
        [Route("Pharmacist/Edit/{userId}")]
        public IActionResult Edit(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(userId);

            if (pharmacist is null)
                return NotFound(); // 404

            var pharmacistViewModel = (PharmacistViewModel)pharmacist;
            return View(pharmacistViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Pharmacist/Edit/{userId}")]
        public IActionResult Edit([FromRoute] string userId, PharmacistViewModel ViewModel)
        {

            if (userId != ViewModel.UserId)
                return BadRequest();//400
            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(ViewModel.UserId);
			if (ModelState.IsValid)
			{
				try
				{
					pharmacist.UpdateInfo(ViewModel);
					//unitOfWork.Repository<Doctor>().Update(doctor);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "pharmacist update successfully!";

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during update pharmacist";
					return View(ViewModel);
				}
			}

			return View(ViewModel);
		}
		#endregion
	}
}
