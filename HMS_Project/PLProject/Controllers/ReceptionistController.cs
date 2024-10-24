using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PLProject.Controllers
{
    public class ReceptionistController : Controller
    {
        #region DPI
        
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment env;

        public ReceptionistController(IUnitOfWork unitOfWork, IWebHostEnvironment _env)
        {
          
            this.unitOfWork = unitOfWork;
            env = _env;
        }
        #endregion

        #region Index
    [Authorize(Roles = Roles.Admin)]
        public IActionResult Index()
        {
            var Receptionists = unitOfWork.Repository<Receptionist>().GetALL();
            var ReceptionistsViewModels = Receptionists.Select(R => (ReceptionistViewModel)R).ToList();
            return View(ReceptionistsViewModels);
        }
		#endregion

		#region Details
		[Authorize(Roles = Roles.Admin + "," + Roles.Receptionist)] 

		[Route("Receptionist/Details/{userId}")]
        public IActionResult Details(string userId, string viewname = "Details")
        {
            if (userId is null)
                return BadRequest(); // 400

            var receptionist = unitOfWork.Repository<Receptionist>().Get(userId);
            var ReceptionistView = (ReceptionistViewModel)receptionist;

            if (receptionist is null)
                return NotFound(); // 404
			if (User.IsInRole(Roles.Doctor))
				return RedirectToAction(nameof(Index), controllerName: "Home");
			else
				return View(viewname,ReceptionistView);
        }

		#endregion

		#region Edit
		[Authorize(Roles = Roles.Admin + "," + Roles.Receptionist)]

		[Route("Receptionist/Edit/{userId}")]
        public IActionResult Edit(string userId)
        {
            if (userId is null)
                return BadRequest(); // 400

            var receptionist = unitOfWork.Repository<Receptionist>().Get(userId);
            if (receptionist is null)
                return NotFound(); // 404

            var receptionistViewModel = (ReceptionistViewModel)receptionist;
            return View(receptionistViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Route("Receptionist/Edit/{userId}")]
        public IActionResult Edit([FromRoute] string userId, ReceptionistViewModel ViewModel)
        {

            if (userId != ViewModel.UserId)
                return BadRequest();//400
            var receptionist = unitOfWork.Repository<Receptionist>().Get(ViewModel.UserId);
			if (ModelState.IsValid)
			{
				try
				{
					receptionist.UpdateInfo(ViewModel);
					//unitOfWork.Repository<Doctor>().Update(doctor);
					unitOfWork.Complete();

					// Set a success message using TempData
					TempData["SuccessMessage"] = "receptionist update successfully!";

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					if (env.IsDevelopment())
						ModelState.AddModelError(string.Empty, ex.Message);
					else
						// Set an error message using TempData
						TempData["ErrorMessage"] = "An Error Has Occurred during update receptionist";
					return View(ViewModel);
				}
			}

			return View(ViewModel);
		}
        #endregion
    }
}
