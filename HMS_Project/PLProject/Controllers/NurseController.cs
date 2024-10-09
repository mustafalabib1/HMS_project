using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class NurseController : Controller
    {
        private readonly IRepository<Nurse> NurseRepo;

        public NurseController(IRepository<Nurse> NurseRepo)
        {
            this.NurseRepo = NurseRepo;
        }


        public IActionResult Index()
        {
            var Nurses = NurseRepo.GetALL();
            var NurseViewModels = Nurses.Select(p => (NurseViewModel)p).ToList();
            return View(NurseViewModels);
        }


        #region Create
        public IActionResult Create()
        {
            return View(new NurseViewModel());
        }

        [HttpPost]
        public IActionResult Create(NurseViewModel NurseViewModel)
        {
            if (ModelState.IsValid) // server side validation
            {
                NurseRepo.Add((Nurse)NurseViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(NurseViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Nurse = NurseRepo.Get(Id.Value);
            var NurseViewModel = (NurseViewModel)Nurse;

            if (Nurse is null)
                return NotFound(); // 404

            return View(NurseViewModel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Nurse = NurseRepo.Get(Id.Value);

            if (Nurse is null)
                return NotFound(); // 404

            var NurseViewModel = (NurseViewModel)Nurse;
            return View(NurseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(NurseViewModel NurseViewModel)
        {
            var Nurse = NurseRepo.Get(NurseViewModel.Id);
            NurseViewModel.UserPassword = Nurse.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                NurseRepo.Update((Nurse)NurseViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(NurseViewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var Nurse = NurseRepo.Get(Id.Value);
            var NurseViewModel = (NurseViewModel)Nurse;

            if (Nurse is null)
                return NotFound(); // 404

            return View(NurseViewModel);
        }

        [HttpPost]
        public IActionResult Delete(NurseViewModel NurseViewModel)
        {
            var Nurse = NurseRepo.Get(NurseViewModel.Id);
            try
            {
                NurseRepo.Delete(Nurse);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(NurseViewModel);
            }
        }
        #endregion
    }
}
