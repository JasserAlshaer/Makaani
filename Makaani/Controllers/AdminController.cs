using Microsoft.AspNetCore.Mvc;

namespace Makaani.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
