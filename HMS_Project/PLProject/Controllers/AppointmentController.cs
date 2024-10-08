using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IRepository<Apointment> appointmentRepo;
        private readonly IRepository<Clinic> clinicRepo;
        private readonly IRepository<Receptionist> receptionistRepo;
        private readonly IRepository<Doctor> doctorRepo;

        public AppointmentController(IRepository<Apointment> appointmentRepo,
            IRepository<Clinic> clinicRepo,
            IRepository<Receptionist> receptionistRepo,
            IRepository<Doctor> doctorRepo) 
        { 
            this.appointmentRepo = appointmentRepo;
            this.clinicRepo = clinicRepo;
            this.receptionistRepo = receptionistRepo;
            this.doctorRepo = doctorRepo;
        }

        public IActionResult Index()
        {
            var appointments = appointmentRepo.GetALL();
            var appointmentViewModels = appointments.Select(a => (ApointmentViewModel)a).ToList();
            return View(appointmentViewModels);
        }

        public IActionResult Create()
        {
            var appointmentViewModel = new ApointmentViewModel() 
            { 
                ApointmentStatus = ApointmentStatusEnum.Scheduled.ToString(),
                Clinics = clinicRepo.GetALL(),
                Receptionists = receptionistRepo.GetALL(),
                Doctors = doctorRepo.GetALL(),
            };
            return View(appointmentViewModel);
        }

        [HttpPost]
        public IActionResult Create(ApointmentViewModel model)
        {
            return View();
        }
    }
}
