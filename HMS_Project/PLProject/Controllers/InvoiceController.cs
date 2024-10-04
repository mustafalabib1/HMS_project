using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
