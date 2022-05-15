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
            var Pmedia = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var Pfinishes = _context.Finishes.ToList();
            var category = _context.Category.ToList();
            var department = _context.Department.ToList();
            var Place = _context.Provinces.ToList();
            var location = _context.Location.ToList();
            var users = _context.User.Where(x => x.UserId != HttpContext.Session.GetInt32("Id")).ToList();
            var promoution = _context.Promotion.ToList();
            var offer = _context.Offer.ToList();
            var media = _context.Media.ToList();
            var finishes = _context.Finishes.ToList();
            var product = _context.Product.ToList();
            var ads = _context.Ads.ToList();


            var estateCardJoinData = from p in product
                                     join
                                     f in finishes on p.FinishesId equals f.FinishesId
                                     join
                                      m in media on p.ProductId equals m.ProductId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,

                                     };

            var estateFullAdsInfo = from a in ads
                                    join
                                    u in users on a.UserId equals u.UserId
                                    join
                                    l in location on a.LocationId equals l.LoactionId
                                    join
                                    d in department on a.DepartmentId equals d.DepartmentId
                                    join
                                    c in category on a.CategoryId equals c.CategoryId
                                    join
                                    e in estateCardJoinData on a.ProductId equals e.Product.ProductId



                                    select new AdsCard
                                    {
                                        Ads = a,
                                        User = u,
                                        Location = l,
                                        Department = d,
                                        Category = c,
                                        EstateMain = e,

                                    };
            return View(estateFullAdsInfo);
        }
        public IActionResult Users()
        {
            return View(_context.User.ToList());
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
        public IActionResult Products()
        {
            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var finishes = _context.Finishes.ToList();
            var product = _context.Product.ToList();


            var estateCardJoinData = from p in product
                                     join
                    f in finishes on p.FinishesId equals f.FinishesId
                                     join
                                  m in media on p.ProductId equals m.ProductId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,

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
