using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PLProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
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
        public IActionResult Index()
        {
            var Receptionists = unitOfWork.Repository<Receptionist>().GetALL();
            var ReceptionistsViewModels = Receptionists.Select(R => (ReceptionistViewModel)R).ToList();
            return View(ReceptionistsViewModels);
        }
        #endregion

        #region Details
        [Route("Receptionist/Details/{userId}")]
        public IActionResult Details(string userId, string viewname = "Details")
        {
            if (userId is null)
                return BadRequest(); // 400

            var receptionist = unitOfWork.Repository<Receptionist>().Get(userId);
            var ReceptionistView = (ReceptionistViewModel)receptionist;

            if (receptionist is null)
                return NotFound(); // 404

            return View(viewname,ReceptionistView);
        }

        #endregion

        #region Edit
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

        [HttpPost]
        [Route("Receptionist/Edit/{userId}")]
        public IActionResult Edit(ReceptionistViewModel receptionistViewModel)
        {
            var receptionist = unitOfWork.Repository<Receptionist>().Get(receptionistViewModel.UserId);
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Receptionist>().Update((Receptionist)receptionistViewModel);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(receptionistViewModel);
        }


        #endregion
    }
}
