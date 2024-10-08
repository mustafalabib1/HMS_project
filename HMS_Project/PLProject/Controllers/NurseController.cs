using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class NurseController : Controller
    {
        private readonly IRepository<Nurse> nurseRepo;

        public IRepository<DoctorSpecializationLookup> SpecializationRepo { get; }

        public NurseController(IRepository<Nurse> SpecializationRepo, IRepository<Nurse> NurseRepo)
        {
            this.nurseRepo = SpecializationRepo;
            nurseRepo = NurseRepo;
        }
        public IActionResult Index()
        {
            var nurses = nurseRepo.GetALL();
            var nurseViewModels = nurses.Select(c => (NurseViewModel)c).ToList();
            return View(nurseViewModels);
        }


        #region create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Nurse nurse)
        {
            if (ModelState.IsValid) // server side validation
            {
                var count = nurseRepo.Add((nurse));
                if (count > 0)
                    return RedirectToAction(nameof(Index)/*"Index"*/);
            }
            return View(nurse); 
        }
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); // 400

            var nurse = nurseRepo.Get(Id.Value);
            var nurseViewModel = (NurseViewModel)nurse;

            if (nurse is null)
                return NotFound(); // 404

            return View(nurseViewModel);
        }

        #endregion


    }

}
