using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly IRepository<Pharmacist> pharmacistRepo;

        public PharmacistController(IRepository<Pharmacist> pharmacistRepo)
        {
            this.pharmacistRepo = pharmacistRepo;
        }


        public IActionResult Index()
        {
            var pharmacists = pharmacistRepo.GetALL();
            var pharmacistViewModels = pharmacists.Select(p => (PharmacistViewModel)p).ToList();
            return View(pharmacistViewModels);
        }


        #region Create
        public IActionResult Create()
        {
            return View(new PharmacistViewModel());
        }

        [HttpPost]
        public IActionResult Create(PharmacistViewModel pharmacistViewModel)
        {
            if (ModelState.IsValid) // server side validation
            {
                pharmacistRepo.Add((Pharmacist)pharmacistViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacistViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var pharmacist = pharmacistRepo.Get(Id.Value);
            var pharmacistViewModel = (PharmacistViewModel)pharmacist;

            if (pharmacist is null)
                return NotFound(); // 404

            return View(pharmacistViewModel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var pharmacist = pharmacistRepo.Get(Id.Value);

            if (pharmacist is null)
                return NotFound(); // 404

            var pharmacistViewModel = (PharmacistViewModel)pharmacist;
            return View(pharmacistViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PharmacistViewModel pharmacistViewModel)
        {
            var pharmacist = pharmacistRepo.Get(pharmacistViewModel.Id);
            pharmacistViewModel.UserPassword = pharmacist.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                pharmacistRepo.Update((Pharmacist)pharmacistViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacistViewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var pharmacist = pharmacistRepo.Get(Id.Value);
            var pharmacistViewModel = (PharmacistViewModel)pharmacist;

            if (pharmacist is null)
                return NotFound(); // 404

            return View(pharmacistViewModel);
        }

        [HttpPost]
        public IActionResult Delete(PharmacistViewModel pharmacistViewModel)
        {
            var pharmacist = pharmacistRepo.Get(pharmacistViewModel.Id);
            try
            {
                pharmacistRepo.Delete(pharmacist);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(pharmacistViewModel);
            }
        }
        #endregion
    }
}
