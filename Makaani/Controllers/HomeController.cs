using Makaani.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Makaani.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MakaniContext _context;

        public HomeController(MakaniContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Estate()
        {
            return View();
        }

        public IActionResult Categorys()
        {
            return View();
        }

        public IActionResult TopSalers()
        {
            return View();
        }

        public IActionResult Testimonials()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

    }
}
