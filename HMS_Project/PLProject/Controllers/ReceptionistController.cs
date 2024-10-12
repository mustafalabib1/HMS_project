using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PLProject.Helpers;

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

        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ReceptionistViewModel receptionist)
        {
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Receptionist>().Add((Receptionist)receptionist);
                 return RedirectToAction(nameof(Index));
            }
            return View(receptionist);
        }
    
        #endregion

        #region Details
        public IActionResult Details(int? Id, string viewname = "Details")
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var receptionist = unitOfWork.Repository<Receptionist>().Get(Id.Value);
            var ReceptionistView = (ReceptionistViewModel)receptionist;

            if (receptionist is null)
                return NotFound(); // 404

            return View(viewname,ReceptionistView);
        }

        #endregion

        #region Edit

        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var receptionist = unitOfWork.Repository<Receptionist>().Get(Id.Value);
            if (receptionist is null)
                return NotFound(); // 404

            var receptionistViewModel = (ReceptionistViewModel)receptionist;
            return View(receptionistViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ReceptionistViewModel receptionistViewModel)
        {
            var receptionist = unitOfWork.Repository<Receptionist>().Get(receptionistViewModel.Id);
            receptionistViewModel.UserPassword = receptionist.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Receptionist>().Update((Receptionist)receptionistViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(receptionistViewModel);
        }


        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {

            return Details(Id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(ReceptionistViewModel receptionistViewModel)
        {
            var receptionist = unitOfWork.Repository<Receptionist>().Get(receptionistViewModel.Id);
            try
            {
                unitOfWork.Repository<Receptionist>().Delete(receptionist);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Deleting the Department");

                return View(receptionistViewModel);
            }
        }
        #endregion
    }
}
