using Makaani.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                ViewBag.Ads = _context.Ads.Count();
                ViewBag.Customer = _context.Login.Where(x => x.RoleId == 1).Count();
                ViewBag.Testimonoials = _context.Testimonails.Count();
                ViewBag.Seller = _context.Login.Where(x => x.RoleId == 2).Count();
                ViewBag.Searches = _context.UserSearch.Count();
                ViewBag.Last = _context.LastViewAds.Count();
                ViewBag.PayOffer = _context.PayingOffer.Count();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Users()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return View(_context.User.Where(x=>x.UserId!=6).OrderByDescending(x => x.JoiningDate).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Testimonials()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                var users = _context.User.ToList();
                var testiomonails = _context.Testimonails.Where(t=>t.IsAccepted!=true).ToList();

                var UserFeeds = from u in users
                                join t in testiomonails
                on u.UserId equals t.UserId
                                select new Feed
                                {
                                    User = u,
                                    Testimonails = t
                                };
                return View(UserFeeds);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult RejectTesitominals(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                var test = _context.Testimonails.Where(x => x.TestimonailsId == id).SingleOrDefault();
                if (test == null)
                {
                    return NotFound();
                }
                test.IsAccepted = false;
                _context.Update(test);
                _context.SaveChanges();
                return RedirectToAction("Testimonials");

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult AcceptTesitominals(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                var test = _context.Testimonails.Where(x => x.TestimonailsId == id).SingleOrDefault();
                if (test == null)
                {
                    return NotFound();
                }
                test.IsAccepted = true;
                _context.Update(test);
                _context.SaveChanges();

                return RedirectToAction("Testimonials");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult Products()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                var EsatateCategory = _context.Category.ToList();
                var Place = _context.Provinces.ToList();

                var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
                var finishes = _context.Finishes.ToList();
                var product = _context.Product.ToList();
                var ads = _context.Ads.ToList();
                var depar = _context.Department.ToList();
                var owners = _context.User.ToList();
                var locations = _context.Location.ToList();

                var estateCardJoinData = from p in product
                                         join
                                         f in finishes on p.FinishesId equals f.FinishesId
                                         join
                                          m in media on p.ProductId equals m.ProductId
                                         join a in ads on p.ProductId equals a.ProductId
                                         join o in owners on a.UserId equals o.UserId
                                         join d in depar on a.DepartmentId equals d.DepartmentId
                                         join l in locations on a.LocationId equals l.LoactionId
                                         select new EstateMain
                                         {
                                             Product = p,
                                             Finishes = f,
                                             Media = m,
                                             Ads = a,
                                             User = o,
                                             Department = d,
                                             Location = l

                                         };
                return View(estateCardJoinData);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Clear();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        //#83BD75
    }
}
