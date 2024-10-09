using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class ReceptionistController : Controller
    {
        #region DPI
        private readonly IRepository<Receptionist> receptionistRepo;
        private readonly IWebHostEnvironment env;

        public ReceptionistController(IRepository<Receptionist> ReceptionistCRepo, IWebHostEnvironment _env)
        {
            receptionistRepo = ReceptionistCRepo;
            env = _env;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var Receptionists = receptionistRepo.GetALL();
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
                var count = receptionistRepo.Add((Receptionist)receptionist);
                if (count > 0)
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

            var receptionist = receptionistRepo.Get(Id.Value);
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

            var receptionist = receptionistRepo.Get(Id.Value);
            if (receptionist is null)
                return NotFound(); // 404

            var receptionistViewModel = (ReceptionistViewModel)receptionist;
            return View(receptionistViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ReceptionistViewModel receptionistViewModel)
        {
            var receptionist =receptionistRepo.Get(receptionistViewModel.Id);
            receptionistViewModel.UserPassword = receptionist.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                receptionistRepo.Update((Receptionist)receptionistViewModel);
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
            var receptionist = receptionistRepo.Get(receptionistViewModel.Id);
            try
            {
                receptionistRepo.Delete(receptionist);
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
