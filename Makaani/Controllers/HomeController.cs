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

        public IActionResult SearchForEstate(string keyWord,int categotyId,int typeId)
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
        [HttpPost]
        public IActionResult Login(String accountUser, String Password)
        {
            
            if (accountUser.Contains("@"))
            {
                var login = _context.Login.Where(x => x.Email == accountUser && x.Password == Password).Single();
                if (login == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return RedirectToAction("Index", "Saler");
                }
            }
            else
            {
                var login = _context.Login.Where(x => x.Phone == accountUser && x.Password == Password).Single();
                if (login == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return RedirectToAction("Index", "Saler");
                }
            }
            
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string FullName, string Address, string Nationality, DateTime? BirthDate,String Email,String Password,String Phone)
        {
            User makamiUser=new User();
            makamiUser.FullName = FullName;
            makamiUser.Address = Address;
            makamiUser.Nationality = Nationality;
            makamiUser.BirthDate = BirthDate;

            _context.Add(makamiUser);
            _context.SaveChanges();

            Login userCredintials=new Login();
            userCredintials.Email = Email;
            userCredintials.Password = Password;
            userCredintials.Phone = Phone;
            userCredintials.RoleId = 1;
            userCredintials.UserId= _context.User.OrderByDescending(x => x.UserId).First().UserId;

            _context.Add(userCredintials);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

    }
}
