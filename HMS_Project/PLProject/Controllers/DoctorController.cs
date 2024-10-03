using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
	public class DoctorController : Controller
	{
        private readonly IRepository<Doctor> doctorRepo;

        public IRepository<DoctorSpecializationLookup> SpecializationRepo { get; }

        public DoctorController(IRepository<DoctorSpecializationLookup> SpecializationRepo,IRepository<Doctor> DoctorRepo)
		{
            this.SpecializationRepo = SpecializationRepo;
            doctorRepo = DoctorRepo;
        }
		public IActionResult Index()
		{
			return RedirectToAction(nameof(Create));
		}
		#region create
		public IActionResult Create()
		{
			var ViewModel = new DoctorViewModel() { SpecializationsDateReader = SpecializationRepo.GetALL() };

			return View(ViewModel);
		}
		[HttpPost]
		public IActionResult Create(DoctorViewModel doctorViewModel)
		{
			if (ModelState.IsValid) // server side validation
			{
				doctorRepo.Add((Doctor)doctorViewModel);
				return RedirectToAction(nameof(Index));
			}
			doctorViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
			return View(doctorViewModel);
		}
		#endregion
	}
}
