using BLLProject.Interfaces;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly IRepository<Receptionist> receptionistRepo;

        public IRepository<Receptionist> SpecializationRepo { get; }

        public ReceptionistController(IRepository<Receptionist> SpecializationRepo, IRepository<Receptionist> ReceptionistCRepo)
        {
            this.SpecializationRepo = SpecializationRepo;
            receptionistRepo = ReceptionistCRepo;
        }
        public IActionResult Index()
        {
            var Receptionists = receptionistRepo.GetALL();
            var ReceptionistsViewModels = Receptionists.Select(R => (Receptionist)R).ToList();
            return View(ReceptionistsViewModels);
        }

        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Receptionist receptionist)
        {
            if (ModelState.IsValid) // server side validation
            {
                var count = receptionistRepo.Add((receptionist));
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(receptionist);
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var receptionist = receptionistRepo.Get(Id.Value);
            var ReceptionistView = (Receptionist)receptionist;

            if (receptionist is null)
                return NotFound(); // 404

            return View(ReceptionistView);
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
            if (ModelState.IsValid) // server side validation
            {
                var receptionist = receptionistRepo.Get(receptionistViewModel.Id);
                receptionist.FullName = $"{receptionistViewModel.FirstName} {receptionistViewModel.MiddleName} {receptionistViewModel.LastName}";
                receptionist.DateOfBirth = receptionistViewModel.DateOfBirth;
                receptionist.Phone = receptionistViewModel.Phone;
                receptionist.Email = receptionistViewModel.Email;
                receptionist.Address = receptionistViewModel.Address;
                receptionist.Gender = receptionistViewModel.Gender.ToString();

                receptionistRepo.Update(receptionist);
                return RedirectToAction(nameof(Index));
            }
            return View(receptionistViewModel);
        }


        #endregion

        #region Delete
        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var receptionist = receptionistRepo.Get(Id.Value);
            var receptionistViewModel = (ReceptionistViewModel)receptionist;

            if (receptionist is null)
                return NotFound(); // 404

            return View(receptionistViewModel);
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
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(receptionist);
            }
        }
        #endregion
    }
}
