using Makaani.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makaani.Controllers
{
    public class SalerController : Controller
    {
        private readonly MakaniContext _context;
        public SalerController(MakaniContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Follower()
        {
            return View();
        }
        public IActionResult LastViewAds()
        {
            return View();
        }
        public IActionResult loveList()
        {
            return View();
        }
        public IActionResult MyAds()
        {
            return View();
        }
        public IActionResult PayingOffer()
        {
            return View();
        }
        public IActionResult Searches()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }

}
