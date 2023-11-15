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
        SignInManager<ApplicationUser> signInManager;

        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private UserRepository urepo;
        public BloodContext _db;
        public Helper help;
        public HomeController(ILogger<HomeController> logger, BloodContext db, SignInManager<ApplicationUser> s, UserManager<ApplicationUser> u)
        {
            _logger = logger;
            urepo =new UserRepository();
            _db = db;
            help =new Helper();
            signInManager = s;
            userManager = u;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewBag.id = 0;
            var a = signInManager.Context.User.HasClaim(s => s.Value == "hospital");

            var d = _db.Users.ToList();
            ViewData["data"]=d;
            return View();
       }
      
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
