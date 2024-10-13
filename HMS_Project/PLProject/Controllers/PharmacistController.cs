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
        private readonly IUnitOfWork unitOfWork;

        public PharmacistController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var pharmacists = unitOfWork.Repository<Pharmacist>().GetALL();
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
                unitOfWork.Repository<Pharmacist>().Add((Pharmacist)pharmacistViewModel);
                unitOfWork.Complete();
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

            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(Id.Value);
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

            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(Id.Value);

            if (pharmacist is null)
                return NotFound(); // 404

            var pharmacistViewModel = (PharmacistViewModel)pharmacist;
            return View(pharmacistViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PharmacistViewModel pharmacistViewModel)
        {
            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(pharmacistViewModel.Id);
            pharmacistViewModel.UserPassword = pharmacist.UserPassword;
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Pharmacist>().Update((Pharmacist)pharmacistViewModel);
                unitOfWork.Complete();
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

            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(Id.Value);
            var pharmacistViewModel = (PharmacistViewModel)pharmacist;

            if (pharmacist is null)
                return NotFound(); // 404

            return View(pharmacistViewModel);
        }

        [HttpPost]
        public IActionResult Delete(PharmacistViewModel pharmacistViewModel)
        {
            var pharmacist = unitOfWork.Repository<Pharmacist>().Get(pharmacistViewModel.Id);
            try
            {
                unitOfWork.Repository<Pharmacist>().Delete(pharmacist);
                unitOfWork.Complete();
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
