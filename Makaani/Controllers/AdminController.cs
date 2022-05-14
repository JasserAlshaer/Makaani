using Makaani.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makaani.Controllers
{
    public class AdminController : Controller
    {
        private readonly MakaniContext _context;
        public AdminController(MakaniContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Testimonials()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
        //#83BD75
    }
}
