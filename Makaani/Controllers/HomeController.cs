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
            //Linq  
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
            var ads = _context.Ads.Where(x=>x.Date <= DateTime.Now.AddDays(7)).ToList();
            var depar=_context.Department.ToList();
            var owners = _context.User.ToList();
            var locations = _context.Location.ToList();

            var estateCardJoinData=     from p in product  join
                                          f in finishes on p.FinishesId equals f.FinishesId join
                                          m in media on p.ProductId equals m.ProductId
                                          join a in ads on p.ProductId equals a.ProductId
                                          join o in owners on a.UserId equals o.UserId
                                          join d in depar on a.DepartmentId equals d.DepartmentId
                                          join l in locations on a.LocationId equals l.LoactionId
                                           select new EstateMain
                                          {
                                              Product = p,
                                              Finishes=f,
                                              Media=m,
                                              Ads=a,
                                              User=o,
                                              Department=d,
                                              Location=l

                                          };
            return View(Tuple.Create(estateCardJoinData, EsatateCategory, Place));
        }
        [HttpPost]
        public IActionResult SearchForEstate(string keyWord,int categotyId,int placeId)
        {
            var EsatateCategory = _context.Category.ToList();
            if (categotyId != 0)
            {
                EsatateCategory = _context.Category.Where(x => x.CategoryId == categotyId).ToList();
            }
            

          
            var Place = _context.Provinces.ToList();
            if(placeId != 0)
            {
                Place = _context.Provinces.Where(x=>x.ProvincesId==placeId).ToList();

            }
            var ads = _context.Ads.ToList();

            if(keyWord!=null && keyWord != "")
            {
                ads = _context.Ads.Where(x=>x.Title.Contains(keyWord)).ToList();
            }

            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var finishes = _context.Finishes.ToList();
            var product = _context.Product.ToList();
            
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
            return View("Estate",Tuple.Create(estateCardJoinData, _context.Category.ToList(), _context.Provinces.ToList()));
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Estate()
        {
            var EsatateCategory = _context.Category.ToList();
            var Place = _context.Provinces.ToList();

            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var finishes = _context.Finishes.ToList();
            var product = _context.Product.ToList();
            var ads = _context.Ads.Where(x => x.Date <= DateTime.Now.AddDays(7)).ToList();
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
            return View(Tuple.Create(estateCardJoinData, EsatateCategory, Place));

        }


        public IActionResult SingleEstate(int id)
        {
            int productId =(int) _context.Ads.Where(x => x.ProductId == id).Single().ProductId;
            var media = _context.Media.Where(x=> x.ProductId== productId).ToList();
          
            var category = _context.Category.ToList();
            var department = _context.Department.ToList();
            var Place = _context.Provinces.ToList();
            var location = _context.Location.ToList();
            var users = _context.User.ToList();
            var promoution = _context.Promotion.ToList();
            var offer = _context.Offer.ToList();
            var finishes = _context.Finishes.ToList();
            var feature=_context.Feature.Where(x=>x.ProductId==productId).ToList();
            var product = _context.Product.Where(x=>x.ProductId == productId).ToList();
            var ads = _context.Ads.Where(x=>x.ProductId==productId).ToList();


            

            var estateFullAdsInfo = from a in ads join
                                    u in users on a.UserId equals u.UserId
                                    join
                                    l in location on a.LocationId equals l.LoactionId
                                    join
                                    d in department on a.DepartmentId equals d.DepartmentId
                                    join
                                    c in category on a.CategoryId equals c.CategoryId
                                    join 
                                    p in product on a.ProductId equals p.ProductId
                                    join 
                                    f in finishes on p.FinishesId equals f.FinishesId
                                    join 
                                    feat in feature on p.ProductId equals feat.ProductId
                                    join 
                                    m in media on a.ProductId equals m.ProductId





                                    select new SingleAds
                                    {
                                        Ads = a,
                                        User = u,
                                        Location = l,
                                        Department = d,
                                        Category = c,
                                        Product = p,
                                        Feature=feat,
                                        Finishes=f,
                                        Media=m

                                    };
            return View(estateFullAdsInfo);
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
        public IActionResult Register(string first,string second, string Address, string Nationality, DateTime? BirthDate,String Email,String Password,String Phone)
        {
            User makamiUser=new User();
            makamiUser.FullName = first+"\t"+ second;
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
