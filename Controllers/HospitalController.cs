using Blood_Donation.Identity;
using Blood_Donation.Models;
using Blood_Donation.Pocos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Controllers
{

    public class HospitalController : Controller
    {
        UserManager<ApplicationUser> userManager;
        RoleManager<ApplicationRoles> roleManager;
        SignInManager<ApplicationUser> signinmanager;
        BloodContext _db;
        public HospitalController(UserManager<ApplicationUser> u, RoleManager<ApplicationRoles> r,SignInManager<ApplicationUser> s, BloodContext db)
        {
            userManager = u;
            roleManager = r;
            signinmanager = s;
            _db = db;
        }

        [Authorize(Roles = "hospital")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BloodGroups> bloods = _db.BloodGroups;
            ViewBag.bloods = bloods.Select(a => new SelectListItem()
            {
                Text =a.blood_group_name,
                Value =a.id.ToString()
            });
            ViewBag.hospital =await userManager.GetUserAsync(signinmanager.Context.User);
            return View();
        }

        public async Task<IActionResult> Signup(int? error)
        {
            ViewBag.errors = null;
            ViewBag.errorcode = null;
            if (error ==1)
            {
                @ViewBag.errorcode = "Invalid Credentials";

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signin(loginDetails login)
        {
            var user = await signinmanager.PasswordSignInAsync(login.email, login.pass, false, false);
            if (user.Succeeded)
            {
                return RedirectToAction("index", "Hospital");
            }
            return RedirectToAction("Signup", "Hospital",new { error=1});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(HospitalViewModel hospD)
        {
            try { 
            if (ModelState.IsValid)
            {
                ApplicationUser u = new ApplicationUser() {
                    fullname = hospD.hospital_name,
                    UserName = hospD.hospital_email,
                    Email = hospD.hospital_email
                };
                IdentityResult hospuser = await userManager.CreateAsync(u,hospD.hospital_conf_password);
                if (hospuser.Succeeded)
                {
                    var role = await roleManager.FindByIdAsync("2");
                        if (role==null)
                        {
                            ViewBag.errors = "Please register with real role";
                            await userManager.DeleteAsync(u);
                            return View();
                        }
                    IdentityResult rolefe = await userManager.AddToRoleAsync(u,role.Name);
                    if (rolefe.Succeeded)
                    {
                            ViewBag.errors = "Please Login Now";

                            return View();
                        }
                }
                    return View();
                }
                else {
                    ViewBag.errors = "Please register with real role";


                    return View();
                }
            }
            catch(Exception e)
            {
                ViewBag.errors = e.Message;


                return View();
            }
        }
    }
}
