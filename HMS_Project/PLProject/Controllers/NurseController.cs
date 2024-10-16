using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
                unitOfWork.Repository<Nurse>().Add((Nurse)NurseViewModel);
                unitOfWork.Complete();
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

            var Nurse = unitOfWork.Repository<Nurse>().Get(Id.Value);
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

            var Nurse = unitOfWork.Repository<Nurse>().Get(Id.Value);

            if (Nurse is null)
                return NotFound(); // 404

            var NurseViewModel = (NurseViewModel)Nurse;
            return View(NurseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(NurseViewModel NurseViewModel)
        {
            var Nurse = unitOfWork.Repository<Nurse>().Get(NurseViewModel.Id);
            if (ModelState.IsValid) // server side validation
            {
                unitOfWork.Repository<Nurse>().Update((Nurse)NurseViewModel);
                unitOfWork.Complete();
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

            var Nurse = unitOfWork.Repository<Nurse>().Get(Id.Value);
            var NurseViewModel = (NurseViewModel)Nurse;

            if (Nurse is null)
                return NotFound(); // 404

            return View(NurseViewModel);
        }

        [HttpPost]
        public IActionResult Delete(NurseViewModel NurseViewModel)
        {
            var Nurse = unitOfWork.Repository<Nurse>().Get(NurseViewModel.Id);
            try
            {
                unitOfWork.Repository<Nurse>().Delete(Nurse);
                unitOfWork.Complete();
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
