using Makaani.Models;
using Microsoft.AspNetCore.Http;
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

            IEnumerable<Feed> Feeds = UserFeeds;

            var EsatateCategory=_context.Category.ToList();
            var Place=_context.Provinces.ToList();

            var media=_context.Media.Where(x=>x.IsMainImage==true && x.MediaTypeId==1).ToList();
            var finishes=_context.Finishes.ToList();
            var product =_context.Product.ToList();


            var estateCardJoinData=     from p in product  join
                                          f in finishes on p.FinishesId equals f.FinishesId join
                                          m in media on p.ProductId equals m.ProductId
                                          select new EstateMain
                                          {
                                              Product = p,
                                              Finishes=f,
                                              Media=m,

                                          };
            return View(estateCardJoinData);
        }

        public IActionResult SearchForEstate(string keyWord,int categotyId,int typeId)
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
            return RedirectToAction("Estate", estateFullAdsInfo);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Estate()
        {
            var Pmedia = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var Pfinishes = _context.Finishes.ToList();
            var category = _context.Category.ToList();
            var department = _context.Department.ToList();
            var Place = _context.Provinces.ToList();
            var location = _context.Location.ToList();
            var users = _context.User.Where(x=> x.UserId!= HttpContext.Session.GetInt32("Id")).ToList();
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


        public IActionResult SingleEstate(int id)
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
            return View(estateFullAdsInfo.ElementAt(0));
        }

        public IActionResult Categorys()
        {
            return View(_context.Category.ToList());
        }

        public IActionResult TopSalers()
        {
            var ads=_context.Ads.ToList();
            var users=_context.User.ToList();   
            List <User> topSallers=new List<User>();
            foreach (User u in users)
            {
                int sum = 0;
                foreach(Ads a in ads)
                {
                    if(u.UserId == a.UserId)
                    {
                        sum++;
                    }
                }
                if (sum > 10)
                {
                    topSallers.Add(u);
                }
            }

            return View(topSallers );
        }

        public IActionResult Testimonials()
        {
            var users=_context.User.ToList();
            var testiomonails=_context.Testimonails.ToList();

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
                    HttpContext.Session.SetInt32("Id", (int)login.UserId);
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
                    HttpContext.Session.SetInt32("Id",(int)login.UserId);
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
