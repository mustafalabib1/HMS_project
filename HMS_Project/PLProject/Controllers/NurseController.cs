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
        private readonly IUnitOfWork unitOfWork;

        public NurseController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var Nurses = unitOfWork.Repository<Nurse>().GetALL();
            var NurseViewModels = Nurses.Select(p => (NurseViewModel)p).ToList();
            return View(NurseViewModels);
        }

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

        [HttpPost]
        [Route("Nurse/Edit/{userId}")]
        public IActionResult Edit(NurseViewModel NurseViewModel)
        {
            var nurse = unitOfWork.Repository<Nurse>().Get(NurseViewModel.UserId);

            if (ModelState.IsValid) // server side validation
            {
                nurse.UpdateInfo(NurseViewModel);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(NurseViewModel);
        }
        #endregion
    }
}
