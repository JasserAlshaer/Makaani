using Microsoft.AspNetCore.Mvc;

namespace Makaani.Controllers
{
    public class SalerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
