using BLLProject.Interfaces;
using DALProject.model;
using PLProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IRepository<Invoice> repository;
        private readonly IRepository<Receptionist> recprepository;
        private readonly IRepository<Apointment> apprepository;

        public InvoiceController(IRepository<Invoice> repository, IRepository<Receptionist> Recprepository, IRepository<Apointment> Apprepository)
        {
            this.repository = repository;
           recprepository = Recprepository;
           apprepository = Apprepository;
        }
        public IActionResult Index()
        {
            var Invoices = repository.GetALL();
            var InvoiceViewModel = Invoices.Select(c => (InvoiceViewModel)c).ToList();
            return View(Invoices);
        }
        #region Create
        public IActionResult Create()
        {
            var invoiceviewmodel = new InvoiceViewModel()
            {
                PaymentType = PaymentType.Cash,
                ReceptionistsReader = recprepository.GetALL(),
                ApointmentsReader = apprepository.GetALL(),

            };
            return View(invoiceviewmodel);
        }

        [HttpPost]
        public IActionResult Create(ApointmentViewModel model)
        {
            return View();
        } 
        #endregion

        #region Details
        public IActionResult Details(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest();

            var invoice = repository.Get(Id.Value);
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

            var invoice = repository.Get(Id.Value);

            if (invoice is null)
                return NotFound();
            var invoiceviewmodel = (InvoiceViewModel)invoice;

            invoiceviewmodel.ReceptionistsReader = recprepository.GetALL();
            invoiceviewmodel.ApointmentsReader = apprepository.GetALL();

            return View(invoiceviewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int Id,InvoiceViewModel invoiceViewModel)
        {
            if (Id != invoiceViewModel.Id) return BadRequest();
            
            Invoice invoice = repository.Get(invoiceViewModel.Id);

            if (ModelState.IsValid) // server side validation
            {
                // Mapping the model
                invoice.InvoiceDate = invoiceViewModel.InvoiceDate;
                invoice.TotalAmount = invoiceViewModel.TotalAmount;
                invoice.ReceptionistId = invoiceViewModel.ReceptionistId;
                invoice.ApointmentId = invoiceViewModel.ApointmentId;
                invoice.PaymentStatus = invoiceViewModel.PaymentStatus;
                invoice.PaymentType = invoiceViewModel.PaymentType.ToString();

                repository.Update(invoice);
                return RedirectToAction(nameof(Index));
            }
            invoiceViewModel.ReceptionistsReader = recprepository.GetALL();
            invoiceViewModel.ApointmentsReader = apprepository.GetALL();
            return View(invoiceViewModel);
        }
        #endregion

        public IActionResult Delete(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest(); 

            var invoice = repository.Get(Id.Value);
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

            var ivoice = repository.Get(invoiceViewModel.Id);
            try
            {
                repository.Delete(ivoice);
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
