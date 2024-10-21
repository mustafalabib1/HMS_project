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

        public PharmacistController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
            if (ModelState.IsValid) // server side validation
            {
                pharmacist.UpdateInfo(ViewModel);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(ViewModel);
        }
        #endregion
    }
}
