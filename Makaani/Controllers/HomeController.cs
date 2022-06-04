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
            var department=_context.Finishes.ToList();

            var media=_context.Media.Where(x=>x.IsMainImage==true && x.MediaTypeId==1).ToList();
            var finishes=_context.Finishes.ToList();
            var product =_context.Product.ToList();
            var ads = _context.Ads.Where(x=>x.Date <= DateTime.Now.AddDays(7)).ToList();
            var depar=_context.Department.ToList();
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
                                     join prov in Place on l.ProvincesId equals prov.ProvincesId
                                     join c in EsatateCategory on a.CategoryId equals c.CategoryId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,
                                         Ads = a,
                                         User = o,
                                         Department = d,
                                         Location = l,
                                         Category = c,
                                         Provinces = prov,

                                     };
            return View(Tuple.Create(estateCardJoinData, EsatateCategory, Place,department));
        }
        [HttpPost]
        public IActionResult SearchForEstate(string keyWord,int budget, int categotyId,int placeId,int finishiesId)
        {

            if (HttpContext.Session.GetInt32("UserId") == null)
            {
               
            }
            else
            {
                UserSearch userSearch = new UserSearch();
                userSearch.SearchTitle = keyWord;
                userSearch.CategoryId = categotyId;
                userSearch.PlaceId = placeId;
                userSearch.UserId = HttpContext.Session.GetInt32("UserId");
                _context.Add(userSearch);
                _context.SaveChanges();
            }


            var EsatateCategory = _context.Category.ToList();
            if (categotyId != 0)
            {
                EsatateCategory = _context.Category.Where(x => x.CategoryId == categotyId).ToList();
            }



            var Place = _context.Provinces.ToList();
            if (placeId != 0)
            {
                Place = _context.Provinces.Where(x => x.ProvincesId == placeId).ToList();

            }
            var ads = _context.Ads.ToList();

            if (keyWord != null && budget != 0)
            {
                ads = _context.Ads.Where(x => x.Title.Contains(keyWord) || x.Price <= budget).ToList();
            }
            else if (keyWord == null && budget != 0)
            {
                ads = _context.Ads.Where(x => x.Price <= budget).ToList();
            }
            else if (keyWord != null && budget == 0)
            {
                ads = _context.Ads.Where(x => x.Title.Contains(keyWord)).ToList();
            }
            var finishes = _context.Finishes.ToList();
            if (finishiesId != 0)
            {
                finishes = _context.Finishes.Where(x => x.FinishesId == finishiesId).ToList();
            }

            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();

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
                                     join prov in Place on l.ProvincesId equals prov.ProvincesId
                                     join c in EsatateCategory on a.CategoryId equals c.CategoryId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,
                                         Ads = a,
                                         User = o,
                                         Department = d,
                                         Location = l,
                                         Category = c,
                                         Provinces = prov,

                                     };
            var department = _context.Finishes.ToList();

            return View("Estate", Tuple.Create(estateCardJoinData, _context.Category.ToList(), _context.Provinces.ToList(), department));
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Estate()
        {
            var EsatateCategory = _context.Category.ToList();
            var Place = _context.Provinces.ToList();
            var department = _context.Finishes.ToList();

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
                                     join prov in Place on l.ProvincesId equals prov.ProvincesId
                                     join c in EsatateCategory on a.CategoryId equals c.CategoryId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,
                                         Ads = a,
                                         User = o,
                                         Department = d,
                                         Location = l,
                                         Category = c,
                                         Provinces = prov,

                                     };
            return View(Tuple.Create(estateCardJoinData, _context.Category.ToList(), _context.Provinces.ToList(), department));

        }


        public IActionResult SingleEstate(int id)
        {

           

            int productId = 0;
            var productObj = _context.Ads.Where(x => x.AdsId == id).SingleOrDefault();
            if(productObj != null)
            {
                productId =(int) productObj.ProductId;
            }
            ViewBag.features = _context.Feature.Where(a => a.ProductId == productId);
           
            var media = _context.Media.Where(x => x.ProductId == productId && x.IsMainImage==true).ToList();
            
            var category = _context.Category.ToList();
            var department = _context.Department.ToList();
            var Place = _context.Provinces.ToList();
            var location = _context.Location.ToList();
            var users = _context.User.ToList();
            //var promoution = _context.Promotion.ToList();
            //var offer = _context.Offer.ToList();
            var finishes = _context.Finishes.ToList();
            var feature = _context.Feature.Where(x => x.ProductId == productId).ToList();
            var product = _context.Product.Where(x => x.ProductId == productId).ToList();
            var ads = _context.Ads.Where(x => x.AdsId == id).ToList();




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
                                        Feature = feat,
                                        Finishes = f,
                                        Media = m

                                    };

            ViewBag.adsViews = _context.LastViewAds.Where(x => x.AdsId == id).Count();
           
            if (HttpContext.Session.GetInt32("UserId") != null)
            {


                var record = _context.LastViewAds.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")
                  && x.AdsId == id).SingleOrDefault();
                if (record == null)
                {
                    LastViewAds lastViewAds = new LastViewAds();
                    lastViewAds.LastViewAt = DateTime.Now;
                    lastViewAds.AdsId = id;
                    lastViewAds.UserId = HttpContext.Session.GetInt32("UserId");
                    _context.Add(lastViewAds);
                    _context.SaveChanges();
                }
                else
                {
                    record.LastViewAt = DateTime.Now;
                    _context.Update(record);
                    _context.SaveChanges(true);

                }
            }
            return View(Tuple.Create(estateFullAdsInfo,_context.Media.ToList()));
        }
        //public IActionResult Image360(string imagePath="")
        //{
        //    if(imagePath == "")
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.imagePath = imagePath;  
        //    return View();
        //}

        //public IActionResult ViedoVR(string videoPath)
        //{
        //    ViewBag.videoPath = videoPath;
        //    return View();
        //}
        public IActionResult Categorys()
        {
            return View(_context.Category.ToList());
        }

        public IActionResult TopSellers()
        {
            var ads=_context.Ads.ToList();
            var users=_context.User.ToList();   
            List <User> topSellers=new List<User>();
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
                if (sum >= 1)
                {
                    topSellers.Add(u);
                }
            }

            return View("TopSellers",topSellers );
        }

        public IActionResult Testimonials()
        {
            var users=_context.User.ToList();
            var testiomonails=_context.Testimonails.Where(x=>x.IsAccepted==true).ToList();

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
            ViewBag.mas = "Login Failed Eaither Email or Password is Wrong";
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Seller");
            }
          
        }
        [HttpPost]
        public IActionResult Login(String accountUser, String Password)
        {
            
            if (accountUser.Contains("@"))
            {
                var login = _context.Login.Where(x => x.Email == accountUser && x.Password == Password).SingleOrDefault();
                if (login == null)
                {
                    return RedirectToAction("Error");
                }
                else
                {

                    HttpContext.Session.SetInt32("UserId", (int)login.UserId);
                    if (login.RoleId == 3)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Seller");
                    }
                   
                }
            }
            else
            {
                var login = _context.Login.Where(x => x.Phone == accountUser && x.Password == Password).SingleOrDefault();
                if (login == null)
                {
                    return RedirectToAction("Error");
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", (int)login.UserId);
                    if (login.RoleId == 3)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Seller");
                }
            }
            
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Seller");
            }
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


        public IActionResult SearchForEstateFromDashboard(string keyWord, int budget, int categotyId, int placeId, int finishiesId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                //UserSearch userSearch   =new UserSearch();
                //userSearch.SearchTitle = keyWord;
                //userSearch.CategoryId = categotyId;
                //userSearch.PlaceId = placeId;
                //userSearch.UserId = 6;
                //_context.Add(userSearch);
                //_context.SaveChanges();
            }
            else
            {
                UserSearch userSearch = new UserSearch();
                userSearch.SearchTitle = keyWord;
                userSearch.CategoryId = categotyId;
                userSearch.PlaceId = placeId;
                userSearch.UserId = HttpContext.Session.GetInt32("UserId");
                _context.Add(userSearch);
                _context.SaveChanges();
            }
                var EsatateCategory = _context.Category.ToList();
            if (categotyId != 0)
            {
                EsatateCategory = _context.Category.Where(x => x.CategoryId == categotyId).ToList();
            }



            var Place = _context.Provinces.ToList();
            if (placeId != 0)
            {
                Place = _context.Provinces.Where(x => x.ProvincesId == placeId).ToList();

            }
            var ads = _context.Ads.ToList();

            if (keyWord != null && budget != 0)
            {
                ads = _context.Ads.Where(x => x.Title.Contains(keyWord) || x.Price <= budget).ToList();
            }
            else if (keyWord == null && budget != 0)
            {
                ads = _context.Ads.Where(x => x.Price <= budget).ToList();
            }
            else if (keyWord != null && budget == 0)
            {
                ads = _context.Ads.Where(x => x.Title.Contains(keyWord)).ToList();
            }
            var finishes = _context.Finishes.ToList();
            if (finishiesId != 0)
            {
                finishes = _context.Finishes.Where(x => x.FinishesId == finishiesId).ToList();
            }

            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();

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
                                     join prov in Place on l.ProvincesId equals prov.ProvincesId
                                     join c in EsatateCategory on a.CategoryId equals c.CategoryId
                                     select new EstateMain
                                     {
                                         Product = p,
                                         Finishes = f,
                                         Media = m,
                                         Ads = a,
                                         User = o,
                                         Department = d,
                                         Location = l,
                                         Category=c,
                                         Provinces = prov,

                                     };
            var department = _context.Finishes.ToList();

            return View("Estate", Tuple.Create(estateCardJoinData, _context.Category.ToList(), _context.Provinces.ToList(), department));
        }
    }
}
