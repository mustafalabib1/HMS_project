using Microsoft.AspNetCore.Mvc;

namespace PLProject.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
