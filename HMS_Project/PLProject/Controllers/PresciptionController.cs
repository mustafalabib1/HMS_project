using BLLProject.Interfaces;
using DALProject.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLProject.ViewModels.Presciption;
using X.PagedList;

namespace PLProject.Controllers
{
	public class PrescriptionController : Controller
	{
        #region DPI
		private readonly IRepository<Prescription> prescriptionRepo;

        public PrescriptionController(IRepository<Prescription> PrescriptionRepo)
        {
            prescriptionRepo = PrescriptionRepo;
        }
        #endregion

        public IActionResult Index(string searchQuery, int? page)
        {

            IEnumerable<Prescription> prescriptions;
            // Filter by ActiveSubstanceName (if provided)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                prescriptions= prescriptionRepo.Find(p=>p.Apointment.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)
                &&p.Apointment.Patient.FullName.ToUpper().Contains(searchQuery.ToUpper())).AsNoTracking().ToList();
            }
            else
            {
                // Fetch all prescriptions entries for this day 
                prescriptions= prescriptionRepo./*Find(p=>p.Apointment.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)).AsNoTracking()*/GetALL().ToList();
            }
            var prescriptionsVM = prescriptions.Select(p => p.ConvertPresciptionToPrescriptionViewModel());
            // Pagination logic
            int pageSize = 10;
            int pageNumber = page ?? 1;

            ViewData["CurrentFilter"] = searchQuery;
            var paginatedList = prescriptionsVM.ToPagedList(pageNumber, pageSize);
            return View(paginatedList);
        }
        #region Create 
        public IActionResult Create()
        {
            return View(new PrescriptionViewModel());
        }

        [HttpPost]
        public IActionResult Create(PrescriptionViewModel model)
        {
            model.DoctorId = 2;
            if (!model.HasItems)
            {
                ModelState.AddModelError(string.Empty, "You must add at least one item.");
            }
            else if (ModelState.IsValid)
            {
                prescriptionRepo.Add(new Prescription().ConvertPrescriptionViewModelToPresciption(model));
            }
            // If the model is invalid, return the view with the same model to show errors
            return View(model);
        } 
        #endregion
    }
}
