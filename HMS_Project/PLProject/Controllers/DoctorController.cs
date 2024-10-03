using DALProject.Data.Contexts;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
	public class DoctorController : Controller
	{
		private readonly HMSdbcontext Context;

		public DoctorController(HMSdbcontext context)
		{
			Context = context;
		}
		public IActionResult Index()
		{
			return RedirectToAction(nameof(Create));
		}
		#region create
		public IActionResult Create()
		{
			var ViewModel = new DoctorViewModel() { SpecializationsDateReader = Context.DoctorSpecializationLookup.ToHashSet() };

			return View(ViewModel);
		}
		[HttpPost]
		public IActionResult Create(DoctorViewModel doctorViewModel)
		{
			if (ModelState.IsValid) // server side validation
			{
				//Context.Doctors.Add((Doctor)doctorViewModel);
				Context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			doctorViewModel.SpecializationsDateReader = Context.DoctorSpecializationLookup.ToHashSet();
			return View(doctorViewModel);
		}
		#endregion
	}
}
