using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLLProject.Specification;
using Microsoft.AspNetCore.Identity;
using PLProject.ViewModels.AppointmentViewModel;

namespace PLProject.Controllers
{
    [Authorize(Roles =$"{Roles.Admin}, {Roles.Receptionist}")]
    public class InvoiceController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public InvoiceController(IUnitOfWork unitOfWork, UserManager<AppUser> UserManager)
        {
            this.unitOfWork = unitOfWork;
            userManager = UserManager;
        }
        public async Task<IActionResult> IndexAsync(int? page)
        {
            // Get the current doctor ID
            var user = await userManager.GetUserAsync(User);

            string UserId = user?.Id ?? string.Empty;

            var spec = new BaseSpecification<Apointment>(/*&&a.ApointmentDate==DateOnly.FromDateTime(DateTime.Now)*/a=> a.ApointmentStatus != ApointmentStatusEnum.Completed && a.ApointmentStatus != ApointmentStatusEnum.Cancelled);
            spec.Includes.Add(a => a.Patient);
            spec.Includes.Add(a => a.Doctor);
            spec.Includes.Add(a => a.Clinic);

            var appointments = unitOfWork.Repository<Apointment>().GetALLWithSpec(spec).ToList();

            var patientappointments = appointments.Select(app => app.ConvertApointmentToAppointmentGenarelVM());
            // Pagination logic
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var paginatedList = patientappointments.ToPagedList(pageNumber, pageSize);

            return View(paginatedList);
        }
        #region Create
        public IActionResult Create()
        {
            var invoiceviewmodel = new InvoiceViewModel()
            {
                PaymentType = PaymentType.Cash,
                ReceptionistsReader = unitOfWork.Repository<Receptionist>().GetALL(),
                ApointmentsReader = unitOfWork.Repository<Apointment>().GetALL(),
               
            };

            return View(invoiceviewmodel);
           
           
        }

        [HttpPost]
        public IActionResult Create(ApointmentViewModel model)
        {
            return View();
            unitOfWork.Complete();

        } 
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest();

            var spec = new BaseSpecification<Invoice>(e => e.Id == Id);
            spec.Includes.Add(e => e.Receptionist);
            spec.Includes.Add(d => d.Apointment);

            var invoice = unitOfWork.Repository<Invoice>().GetEntityWithSpec(spec);
            var invoiceViewModel = (InvoiceViewModel)invoice;

            if (invoice is null)
                return NotFound();

            return View(invoiceViewModel);
        }
        #endregion

        #region Edit
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest();

            var invoice = unitOfWork.Repository<Invoice>().Get(Id.Value);
         

            if (invoice is null)
                return NotFound();
            var invoiceviewmodel = (InvoiceViewModel)invoice;

            invoiceviewmodel.ReceptionistsReader = unitOfWork.Repository<Receptionist>().GetALL();
            invoiceviewmodel.ApointmentsReader = unitOfWork.Repository<Apointment>().GetALL();

            return View(invoiceviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int Id,InvoiceViewModel invoiceViewModel)
        {
            if (Id != invoiceViewModel.Id) return BadRequest();
            
            Invoice invoice = unitOfWork.Repository<Invoice>().Get(invoiceViewModel.Id);

            if (ModelState.IsValid) // server side validation
            {
                // Mapping the model
                invoice.InvoiceDate = invoiceViewModel.InvoiceDate;
                invoice.TotalAmount = invoiceViewModel.TotalAmount;
                invoice.ReceptionistUserId = invoiceViewModel.ReceptionistUserId;
                invoice.ApointmentId = invoiceViewModel.ApointmentId;
                invoice.PaymentStatus = invoiceViewModel.PaymentStatus;
                invoice.PaymentType = invoiceViewModel.PaymentType.ToString();

                unitOfWork.Repository<Invoice>().Update(invoice);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            invoiceViewModel.ReceptionistsReader = unitOfWork.Repository<Receptionist>().GetALL();
            invoiceViewModel.ApointmentsReader = unitOfWork.Repository<Apointment>().GetALL();
            return View(invoiceViewModel);
        }
        #endregion

        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); 

            var invoice = unitOfWork.Repository<Invoice>().Get(Id.Value);
            var invoiceviewmodel = (InvoiceViewModel)invoice;

            if (invoice is null)
                return NotFound();

            return View(invoiceviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int Id,InvoiceViewModel invoiceViewModel)
        {
            if (Id != invoiceViewModel.Id) return BadRequest();

            var ivoice = unitOfWork.Repository<Invoice>().Get(invoiceViewModel.Id);
            try
            {
                //var model = unitOfWork.Repository<Invoice>().Get(invoiceViewModel.Id);
                //if (model.Apointment is not null)
                //{
                //    foreach (var emp in model.Apointment)
                //    {
                //        unitOfWork.Repository<Apointment>().Delete(emp);
                //    }
                //}
             
                unitOfWork.Repository<Invoice>().Delete(ivoice);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(ivoice);
            }
        }
    }
}
