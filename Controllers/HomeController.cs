using Blood_Donation.Helpers;
using Blood_Donation.Identity;
using Blood_Donation.Models;
using Blood_Donation.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blood_Donation.Controllers
{
    [Authorize(Roles = "user")]

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private UserRepository urepo;
        public BloodContext _db;
        public userHelper help;
        public HomeController(ILogger<HomeController> logger, BloodContext db, SignInManager<ApplicationUser> s, UserManager<ApplicationUser> u)
        {
            _logger = logger;
            urepo =new UserRepository();
            _db = db;
            //help =new userHelper();
            signInManager = s;
            userManager = u;
        }
       
        
        public async Task<IActionResult> Index()
        {
            List<hospitalNameID> hosps = new List<hospitalNameID>();
            var stocks = _db.BloodGroupStocks;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ids= stocks.Select(s => s.bloodStockHospital).Distinct().ToList();
            foreach(var id in ids)
            {
                var i=await new userHelper(userManager,signInManager).getUserByid(id);
                hosps.Add(new hospitalNameID()
                {
                    hospital = i.fullname,
                    hospitalId = id,
                    hospitalEmail = i.Email,
                    hospitalAdd = i.Email,
                    stocks = stocks.Where(s => s.bloodStockHospital == id).Select(bl => new BloodGroupStockwithHospital()
                    {
                        bloodid = bl.bloodGroups.id,
                        bloodname = bl.bloodGroups.blood_group_name,
                        bloodQuantity = bl.bloodStockQuantity,
                        reqStatus = new Helper(_db).reqStatus(userId, bl.bloodGroups.id, id)
                    }).ToList()
                }); ; ;
            }
            ViewData["hosps"] = hosps;
            ViewData["userId"] = signInManager.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
       }
        [Route("/accessDeniaed")]
        public IActionResult accessDenied()
        {
            return View();
        }
      
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
