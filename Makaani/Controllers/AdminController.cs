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
            ViewBag.Ads = _context.Ads.Count();
            ViewBag.Customer = _context.Login.Where(x=>x.RoleId==1).Count();
            ViewBag.Testimonoials = _context.Testimonails.Count();
            ViewBag.Saler = _context.Login.Where(x => x.RoleId == 2).Count();
            ViewBag.Searches = _context.UserSearch.ToList(); 
            ViewBag.Last= _context.LastViewAds.Count();
            ViewBag.PayOffer=_context.PayingOffer.Count();
            return View();
        }
        public IActionResult Users()
        {
            return View(_context.User.OrderByDescending(x=>x.JoiningDate).ToList());
        }
        public IActionResult Testimonials()
        {
            var users = _context.User.ToList();
            var testiomonails = _context.Testimonails.ToList();

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
        public IActionResult RejectTesitominals(int id)
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

        public IActionResult AcceptTesitominals(int id)
        {
            var test=_context.Testimonails.Where(x=>x.TestimonailsId == id).SingleOrDefault();
            if (test == null)
            {
                return NotFound();
            }
            test.IsAccepted = true;
            _context.Update(test);
            _context.SaveChanges();
            
            return RedirectToAction("Testimonials");
        }
        public IActionResult Products()
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
        //#83BD75
    }
}
