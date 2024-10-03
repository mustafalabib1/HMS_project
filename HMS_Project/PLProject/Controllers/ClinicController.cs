using BLLProject.Interfaces;
using DALProject.Data.Contexts;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
	public class ClinicController : Controller
	{
		#region DPI
		private readonly IRepository<Clinic> clinicRepo;

		public IRepository<ClinicSpecializationLookup> SpecializationRepo { get; }

		public ClinicController(IRepository<ClinicSpecializationLookup> SpecializationRepo, IRepository<Clinic> ClinicRepo)
		{
			this.SpecializationRepo = SpecializationRepo;
			clinicRepo = ClinicRepo;
		}
        #endregion
        public IActionResult Index()
        {
            var clinics = clinicRepo.GetALL();
            var clinicViewModels = clinics.Select(c => (ClinicViewModel)c).ToList();
            return View(clinicViewModels);
        }

		#region Specialization
		public IActionResult AddSpecialization()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddSpecialization(ClinicSpecializationLookup clinicSpecializationLookup)
		{
			if (ModelState.IsValid) // server side validation
			{
				SpecializationRepo.Add(clinicSpecializationLookup);
				return RedirectToAction(nameof(Index));
			}
			return View(clinicSpecializationLookup);
		} 
		#endregion

		#region Create
		public IActionResult Create()
		{
			var ViewModel = new ClinicViewModel() { SpecializationsDateReader = SpecializationRepo.GetALL() };

			return View(ViewModel);
		}

		[HttpPost]
		public IActionResult Create(ClinicViewModel clinicViewModel)
		{
			if (ModelState.IsValid) // server side validation
			{
				clinicRepo.Add((Clinic)clinicViewModel);
				return RedirectToAction(nameof(Index));
			}
			clinicViewModel.SpecializationsDateReader = SpecializationRepo.GetALL();
			return View(clinicViewModel);
		}
		#endregion


	}
}
