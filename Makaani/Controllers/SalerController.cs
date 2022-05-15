using Makaani.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Makaani.Controllers
{
    public class SalerController : Controller
    {
        private readonly MakaniContext _context;
        private readonly IWebHostEnvironment _env;
        public SalerController(MakaniContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;

        }
        public IActionResult Index()
        {
            ViewBag.Ads = _context.Ads.Where(a => a.UserId == HttpContext.Session.GetInt32("Id")).Count();
            ViewBag.Offers = _context.PayingOffer.Where(a => a.UserId == HttpContext.Session.GetInt32("Id")).Count();
            ViewBag.Follower = _context.Follower.Where(a => a.SecondUserId == HttpContext.Session.GetInt32("Id")).Count();
            ViewBag.LoveList = _context.LoveList.Where(a => a.UserId == HttpContext.Session.GetInt32("Id")).Count();
            return View();
        }
        public IActionResult Profile()
        {
            var users =_context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("Id")).ToList();
            var login =_context.Login.Where(u => u.UserId == HttpContext.Session.GetInt32("Id")).ToList();

            var profile = from u in users
                          join l in login
                          on u.UserId equals l.UserId
                          select new ProfileU
                          {
                              User = u,
                              Login =l
                          };
            return View(profile);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(IFormFile image,string name,string address
            ,string password,string whatsUpLink,string phone,string email)
        {
            var users = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("Id")).Single();
            var login = _context.Login.Where(u => u.UserId == HttpContext.Session.GetInt32("Id")).Single();
            if(users!=null && login != null)
            {
                if(image != null)
                {
                    String wRootPath = _env.WebRootPath;
                    
                    String fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var path1 = Path.Combine(wRootPath + "/Uploads", fileName);
                  
                    using (var filestream = new FileStream(path1, FileMode.Create))
                    {
                        await image.CopyToAsync(filestream);
                    }

                    users.ProfileImage = fileName;
                    users.FullName = name;
                    users.Address = address;
                    users.WhatsupLink= whatsUpLink;
                    login.Phone = phone;
                    login.Password = password;
                    login.Email = email;
                }
                else
                {
                    //users.ProfileImage = fileName;
                    users.FullName = name;
                    users.Address = address;
                    users.WhatsupLink = whatsUpLink;
                    login.Phone = phone;
                    login.Password = password;
                    login.Email = email;
                }

                _context.Update(users);
                _context.Update(login);

                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return View();
        }

        public IActionResult Follower()
        {
            return View(_context.Follower.Where(a => a.SecondUserId == HttpContext.Session.GetInt32("Id")).ToList());
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
            return View(_context.PayingOffer.Where(a => a.UserId == HttpContext.Session.GetInt32("Id")).ToList());
        }
        public IActionResult Searches()
        {
            return View(_context.UserSearch.ToList());
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }

}
