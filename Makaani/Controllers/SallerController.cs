using Makaani.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Makaani.Controllers
{
    public class SallerController : Controller
    {
        private readonly MakaniContext _context;
        private readonly IWebHostEnvironment _env;

        public static int currentProjectId;
        public static int currentLocationId;
        public SallerController(MakaniContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;

        }
        public IActionResult Index()
        {
            
            ViewBag.Ads = _context.Ads.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).Count();
            ViewBag.Offers = _context.PayingOffer.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).Count();
            ViewBag.Follower = _context.Follower.Where(a => a.SecondUserId == HttpContext.Session.GetInt32("UserId")).Count();
            ViewBag.LoveList = _context.LoveList.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).Count();
            
            var latestOffers=_context.PayingOffer.ToList();
            var product=_context.Product.Where(a => a.OwnerId == HttpContext.Session.GetInt32("UserId")).ToList();
            var users = _context.User.ToList();

            var providedOffers=from l in latestOffers join p in product
                               on l.ProductId equals p.ProductId
                               join u in users on l.UserId equals u.UserId
                               select new ProvidedOffers{
                                       User=u,
                                       PayingOffer=l,
                                       Product=p
                                  };

            return View(providedOffers);
        }
        public IActionResult Profile()
        {
            var users =_context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var login =_context.Login.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var role = _context.Role.ToList();
            var profile = from u in users
                          join l in login
                          on u.UserId equals l.UserId
                          join r in role on l.RoleId equals r.RoleId
                          select new ProfileU
                          {
                              User = u,
                              Login =l,
                              Role=r
                          };
            var follower = _context.Follower.Where(a => a.SecondUserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var user = _context.User.Where(x => x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();
            if(follower.Count>0 && user.Count > 0)
            {
                var myFollower = from f in follower
                                 join u in user on f.FirstUserId equals u.UserId
                                 select new Followers
                                 {
                                     Follower = f,
                                     User = u
                                 };
                ViewBag.Follower = myFollower;
                var pro = profile.ElementAt(0);
                return View(pro);
            }
            else {
                ViewBag.Follower = new List<Follower>();    
                var pro = profile.ElementAt(0);
                return View(pro);
            }
          
        }

        public IActionResult MyProducts()
        {
            var EsatateCategory = _context.Category.ToList();
            var Place = _context.Provinces.ToList();

            var media = _context.Media.Where(x => x.IsMainImage == true && x.MediaTypeId == 1).ToList();
            var finishes = _context.Finishes.ToList();
            var product = _context.Product.Where(x => x.OwnerId == HttpContext.Session.GetInt32("UserId")).ToList();
            var ads = _context.Ads.Where(x => x.UserId== HttpContext.Session.GetInt32("UserId")).ToList();
            var depar = _context.Department.ToList();
            var owners = _context.User.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
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

        public IActionResult InsertMedia()
        {
            ViewBag.proId=currentProjectId;
            return View(_context.MediaType.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> InsertMedia(IFormFile image,int mediaType,bool ismain,int product)
        {
            if(image != null)
            {
                String wRootPath = _env.WebRootPath;

                String fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                var path1 = Path.Combine(wRootPath + "/uploads", fileName);

                using (var filestream = new FileStream(path1, FileMode.Create))
                {
                    await image.CopyToAsync(filestream);
                }

                Media media = new Media();
                media.MediaId = _context.Media.OrderByDescending(x => x.MediaId).First().MediaId+1;
                media.MediaTypeId= mediaType;
                media.IsMainImage = ismain;
                media.Path = fileName;
                media.ProductId = product;
                _context.Add(media);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("InsertMedia");
        }

        public IActionResult InsertLoactions()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InsertLoactions(string label,string mapsLink,string note)
        {
            Location location = new Location();
            location.MapsLink = mapsLink;
            location.LoactionLabel = label;
            location.Notes = note;
            _context.Add(location);
            _context.SaveChanges();

            currentLocationId= _context.Location.OrderByDescending(x => x.LoactionId).First().LoactionId;

            return RedirectToAction("InsertAds");
        }
        public IActionResult InsertProducts()
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            if (id != 0)
            {
                var role = _context.Login.Where(x => x.UserId == id).SingleOrDefault();
                if (role == null || role.RoleId != 2)
                {
                    return RedirectToAction("Index");
                }
                return View(_context.Finishes.ToList());
            }
            return RedirectToAction("Index");
               
        }
        [HttpPost]
        public IActionResult InsertProducts(double area,int bathes,int bedroom,int living,int ludary
            ,int grage,int kitchen,int store,bool isBalcon, bool isGarden, bool isElvator,
            bool swimbool,int floor,int finishiesId,bool manyfloor,bool furniture,int floorsum)
        {
            Product product =new Product();
            product.Area = area;
            product.BathRooms = bathes;
            product.Store = store;
            product.BedRooms = bedroom;
            product.LundrayRoom = ludary;
            product.Grage = grage;
            product.Kitchen = kitchen;
            product.IsHasFurniture = furniture;
            product.IshaveBalcon= isBalcon;
            product.IshaveGarden= isGarden;
            product.IshaveElvator= isElvator;   
            product.IshaveSwimPool= swimbool;
            product.FloorNumber= floor;
            product.IsHasManyFloor = manyfloor;
            product.FinishesId = finishiesId;
            product.FloorSum = floorsum;
            product.LivingRoom = living;
            _context.Add(product);
            _context.SaveChanges();


            currentProjectId = _context.Product.OrderByDescending(x => x.ProductId).First().ProductId;

            

            return RedirectToAction("InsertMedia");
        }
        public IActionResult UpdateProducts(int productId)
        {
            var product = _context.Product.Where(l => l.ProductId == productId).Single();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
       [HttpPost]
        public IActionResult UpdateProducts(int productId,double area, int bathes, int bedroom, int living, int ludary
            , int grage, int kitchen, int store, bool isBalcon, bool isGarden, bool isElvator,
            bool swimbool, int floor, int finishiesId, bool manyfloor, bool furniture, int floorsum)
        {
            var product = _context.Product.Where(l => l.ProductId == productId).Single();
            if (product == null)
            {
                return NotFound();
            }
            product.Area = area;
            product.BathRooms = bathes;
            product.Store = store;
            product.BedRooms = bedroom;
            product.LundrayRoom = ludary;
            product.Grage = grage;
            product.Kitchen = kitchen;
            product.IsHasFurniture = furniture;
            product.IshaveBalcon = isBalcon;
            product.IshaveGarden = isGarden;
            product.IshaveElvator = isElvator;
            product.IshaveSwimPool = swimbool;
            product.FloorNumber = floor;
            product.IsHasManyFloor = manyfloor;
            product.FinishesId = finishiesId;
            product.FloorSum = floorsum;
            product.LivingRoom = living;
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("MyProducts");
        }
        public IActionResult DeleteProducts(int productId)
        {
            var prod = _context.Product.Where(l => l.ProductId == productId).Single();
            if (prod == null)
            {
                return NotFound();
            }

            _context.Remove(prod);
            _context.SaveChangesAsync();
            return RedirectToAction("MyProducts");
        }
        public IActionResult UpdateProfile()
        {
            var users = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var login = _context.Login.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var role = _context.Role.ToList();
            var profile = from u in users
                          join l in login
                          on u.UserId equals l.UserId
                          join r in role on l.RoleId equals r.RoleId
                          select new ProfileU
                          {
                              User = u,
                              Login = l,
                              Role = r
                          };
            var follower = _context.Follower.Where(a => a.SecondUserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var user = _context.User.Where(x => x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();
            var myFollower = from f in follower
                             join u in user on f.FirstUserId equals u.UserId
                             select new Followers
                             {
                                 Follower = f,
                                 User = u
                             };
            ViewBag.Follower = myFollower;
            return View(profile.ElementAt(0));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(IFormFile image,string name,string address
            ,string password,string whatsUpLink,string phone,string email)
        {
            var users = _context.User.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).Single();
            var login = _context.Login.Where(u => u.UserId == HttpContext.Session.GetInt32("UserId")).Single();
            if(users!=null && login != null)
            {
                if(image != null)
                {
                    String wRootPath = _env.WebRootPath;
                    
                    String fileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    var path1 = Path.Combine(wRootPath + "/uploads", fileName);
                  
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
            return RedirectToAction("Profile");
        }

        public IActionResult Follower()
        {
            var follower = _context.Follower.Where(a => a.FirstUserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var user =_context.User.Where(x=>x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();
            var myFollower = from f in follower
                             join u in user on f.FirstUserId equals u.UserId
                             select new Followers
                             {
                                 Follower = f,
                                 User = u
                             };
            return View(myFollower);
        }
        public IActionResult FollowNewUser(int seondUserId)
        {
            Follower follower = new Follower();
            follower.FirstUserId=HttpContext.Session.GetInt32("UserId");
            follower.SecondUserId = seondUserId;
            _context.Add(follower);
            _context.SaveChanges();
            return RedirectToAction("Follower");
        }

        public IActionResult UnFollowUser(int follwerId)
        {
            var recorde=_context.Follower.Where(x=>x.FollowerId == follwerId).Single();
            if(recorde == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(recorde);
                _context.SaveChanges();
                return RedirectToAction("Follower");
            }
        }
        public IActionResult LastViewAds()
        {
            var last = _context.LastViewAds.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var ads= _context.Ads.Where(x => x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();

            var lastViewAds = from l in last
                              join a in ads
               on l.AdsId equals a.AdsId
                              select new LastAds
                              {
                                  Ads=a,
                                  LastViewAds=l
                              };
            return View(lastViewAds);
        }
        public async Task<IActionResult> ClearLastViewAds()
        {
            List<LastViewAds>lastViewAds=_context.LastViewAds.Where(l=>l.UserId==HttpContext.Session.GetInt32("UserId")).ToList();
            foreach(var lastViewAd in lastViewAds)
            {
                _context.Remove(lastViewAd);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveLastViewAdsRecord(int recordId)
        {
            var lastViewAds = _context.LastViewAds.Where(l => l.LastViewAdsId == recordId).Single();
           if(lastViewAds == null)
            {
                return NotFound();
            }

           _context.Remove(lastViewAds);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult LoveList()
        {
            int lovelistId=_context.LoveList.Where(x=>x.UserId == HttpContext.Session.GetInt32("UserId")).Single().LoveListId;
            var loveItems = _context.LovedProductList.Where(x => x.LoveListId == lovelistId).ToList();
            var ads=_context.Ads.ToList();
            var product = _context.Product.ToList();

            var myLoveList = from l in loveItems
                             join p in product on
         l.ProductId equals p.ProductId
                             join a in ads on p.ProductId equals a.ProductId
                             select new LoveListItem
                             {
                                 LovedProductList=l,
                                 Product=p,
                                 Ads=a
                             };
            return View(myLoveList);
        }
        public IActionResult InsertToLoveList(int productId)
        {
            var recorde = _context.LoveList.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            if (recorde == null)
            {
                LoveList loveList=new LoveList();
                loveList.UserId =(int) HttpContext.Session.GetInt32("Id");
                _context.Add(loveList);
                _context.SaveChanges();


                LovedProductList lovedProduct = new LovedProductList();
                lovedProduct.ProductId = productId;
                lovedProduct.LoveListId = _context.LoveList.OrderByDescending(x=>x.LoveListId).First().LoveListId;
                _context.Add(lovedProduct);
                _context.SaveChanges();
            }
            else
            {
                LovedProductList lovedProduct = new LovedProductList();
                lovedProduct.ProductId = productId;
                lovedProduct.LoveListId = recorde.LoveListId;
                _context.Add(lovedProduct);
                _context.SaveChanges();

            }
            return RedirectToAction("LoveList");
        }
        public IActionResult DeleteFromLoveList(int productId)
        {
            var recorde = _context.LovedProductList.Where(x => x.ProductId == productId).Single();
            if (recorde == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(recorde);
                _context.SaveChanges();
               
            }
            return RedirectToAction("LoveList");
        }
        //step-4
        public IActionResult InsertAds()
        {
            ViewBag.cat = _context.Category.ToList();
            ViewBag.dep = _context.Department.ToList();
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> InsertAds( string title, double price, string description
            , int promationId, int OfferId,int categotyId,int departmentId)
        {
            int id = (int)HttpContext.Session.GetInt32("UserId");
            if(id != 0)
            {
                var role = _context.Login.Where(x => x.UserId == id).SingleOrDefault();
                if (role == null || role.RoleId!=2)
                {
                    return Unauthorized();
                }
                Ads ad = new Ads();
                ad.ProductId = currentProjectId;
                ad.Descrption = description;
                ad.Price = price;
                ad.CategoryId = categotyId;
                ad.PromotionId = promationId;
                ad.DepartmentId = departmentId;
                ad.Title = title;
                ad.LocationId = currentLocationId;
                
                _context.Ads.Add(ad);


                await _context.SaveChangesAsync();
                return RedirectToAction("MyProducts");
            }
            else
            {
                return NotFound();
            }

           
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdsMainInfo(int adsId,string title,double price,string description
            ,int promationId,int offerId)
        {

            var ad = _context.Ads.Where(x => x.AdsId == adsId).Single();
            if (ad != null)
            {
                ad.Descrption = description;
                ad.Price = price;
                ad.Title = title;
                ad.PromotionId = promationId;
                ad.OfferId = offerId;
                _context.Update(ad);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

                await _context.SaveChangesAsync();
               return RedirectToAction("MyProducts");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdsLocationInfo(int adsId,string label , string mapslink
            ,int regionId,int provinceId,string note)
        {
           

            var ad = _context.Ads.Where(x => x.AdsId == adsId).Single();
            if (ad != null)
            {
                Location location = new Location();
                location.LoactionLabel = label;
                location.MapsLink = mapslink;
                location.RegoinId = regionId;
                location.ProvincesId = provinceId;
                location.Notes = note;

                _context.Add(location);
                _context.SaveChanges();


                ad.LocationId=_context.Location.OrderByDescending(x=>x.LoactionId).First().LoactionId;
                _context.Update(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyProducts");
            }
            else
            {
                return NotFound();
            } 
        }



        public IActionResult OutGoingPayingOffer()
        {
            var offers = _context.PayingOffer.Where(a => a.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
            var users = _context.User.Where(x => x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();
            var product = _context.Product.Where(x => x.OwnerId != HttpContext.Session.GetInt32("UserId")).ToList();
            var ads = _context.Ads.Where(x => x.UserId != HttpContext.Session.GetInt32("UserId")).ToList();

            var recivedPayOffers = from o in offers
                                   join p in product on o.ProductId equals p.ProductId
                                   join a in ads on o.ProductId equals a.ProductId
                                   join u in users on a.UserId equals u.UserId
                                   select new UsersPayingOffers
                                   {
                                       Product = p,
                                       Ads = a,
                                       PayingOffer = o,
                                       User = u
                                   };
            return View(recivedPayOffers);
            //return View(offers);
        }

        public IActionResult SendPayOffer(int adsId)
        {
            var ads = _context.Ads.Where(x => x.AdsId == adsId).SingleOrDefault();
            return View(ads);
        }

       [HttpPost]
        public IActionResult SendPayOffer(int productId,string note, string title,double ? price =0)
        {
            PayingOffer payingOffer = new PayingOffer();
            payingOffer.ProductId = productId;
            payingOffer.Note = note;
            payingOffer.Title = title;
            payingOffer.ProvidedPrice= price;
            payingOffer.OfferDate= DateTime.Now;
            payingOffer.UserId= HttpContext.Session.GetInt32("UserId");
            _context.Add(payingOffer);
            _context.SaveChanges();
            return RedirectToAction("PayingOffer", payingOffer);
        }
        public IActionResult UpdatePayOffer(int offerId)
        {
            var payingOffer = _context.PayingOffer.Where(x => x.PayingOfferId == offerId).Single();
            if (payingOffer == null)
            {
                return NotFound();
            }
            return View(payingOffer);
        }


       [HttpPost]
        public IActionResult UpdatePayOffer(int offerId, string note, string title, double? price = 0 )
        {
            var payingOffer = _context.PayingOffer.Where(x => x.PayingOfferId == offerId).Single();
            if (payingOffer == null)
            {
                return NotFound();
            }
            else
            {
                payingOffer.Note = note;
                payingOffer.ProvidedPrice = price;
                payingOffer.OfferDate = DateTime.Now;
                payingOffer.UserId = HttpContext.Session.GetInt32("UserId");
                _context.Add(payingOffer);
                _context.SaveChanges();
            }
            return RedirectToAction("OutGoingPayingOffer");
        }
        public IActionResult DeletePayOffer(int offerId)
        {
            var recorde = _context.PayingOffer.Where(x => x.PayingOfferId == offerId).Single();
            if (recorde == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(recorde);
                _context.SaveChanges();

            }
            return RedirectToAction("OutGoingPayingOffer");

        }
        public IActionResult Searches()
        {
            return View(_context.UserSearch.ToList());
        }

        public IActionResult DeleteSearch(bool deleteAll,int searchId)
        {
            if (deleteAll)
            {
                List<UserSearch> searches = _context.UserSearch.Where(x=> x.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
                foreach (UserSearch search in searches)
                {
                    _context.Remove(search);
                    _context.SaveChanges();
                }
               
            }
            else
            {
                var recorde = _context.UserSearch.Where(x => x.UserSearchId == searchId).Single();
                if (recorde == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Remove(recorde);
                    _context.SaveChanges();

                }
                
            }
            return RedirectToAction("Searches");
        }
     
        public IActionResult InsertTesti()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertTesti(string text,string desc)
        {
            Testimonails testimonails = new Testimonails(); 
            testimonails.Title = text;
            testimonails.Description= desc;
            testimonails.UserId= HttpContext.Session.GetInt32("UserId");
            testimonails.IsAccepted = false;

            _context.Add(testimonails);
            _context.SaveChanges();

            return View("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }

}
