using BLLProject.Interfaces;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IRepository<Apointment> appointmentRepo;
        public AppointmentController(IRepository<Apointment> appointmentRepo) 
        { 
            this.appointmentRepo = appointmentRepo;
        }

        public IActionResult Index()
        {
            var appointments = appointmentRepo.GetALL();
            return View(appointments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ApointmentViewModel model)
        {
            return View();
        }
    }
}
