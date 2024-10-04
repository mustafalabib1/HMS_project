using BLLProject.Interfaces;
using DALProject.model;
using HMS_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IRepository<Invoice> repository;

        public InvoiceController(IRepository<Invoice> repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var Invoices = repository.GetALL();
            var InvoiceViewModel = Invoices.Select(c => (InvoiceViewModel)c).ToList();
            return View(Invoices);
        }
    }
}
