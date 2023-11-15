using Blood_Donation.Helpers;
using Blood_Donation.Identity;
using Blood_Donation.Models;
using Blood_Donation.Pocos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        codeExec sc = new codeExec();
        BloodContext _db;
        UserManager<ApplicationUser> userManager;
        RoleManager<ApplicationRoles> roleManager;
        SignInManager<ApplicationUser> signInManager;
        public UserController(BloodContext db, UserManager<ApplicationUser> u, RoleManager<ApplicationRoles> r,SignInManager<ApplicationUser> s)
        {
            _db = db;
            this.userManager = u;
            this.roleManager = r;
            this.signInManager = s;
        }
        public IEnumerable<BloodGroups> getBloodGroups()
        {
            return _db.BloodGroups;
        }

        [Route("signin")]
        public IActionResult Index(int? error)
        {
            var a = signInManager.IsSignedIn(this.User);
            if (error!=null)
            {
                ViewData["Error"] = "Login Credential incorrect";
            }
            ViewData["allBloodGrups"] = getBloodGroups().Select(BloodGroups=>new SelectListItem() { 
            Value=Convert.ToString(BloodGroups.id),
            Text=BloodGroups.blood_group_name
            });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("saveUser")]
      public async Task<IActionResult> signupUSerAsync(UserViewModel user)
        {
            user.ApplicationRoleId = "1";
            if (ModelState.IsValid)
            {
                ApplicationUser useraa = new ApplicationUser()
                {
                    fullname = user.Name,
                    Email = user.Email,
                    UserName = user.Email
                };
                IdentityResult res = await userManager.CreateAsync(useraa, user.ConfirmPassword);
                if (res.Succeeded)
                {
                    ApplicationRoles approle = await roleManager.FindByIdAsync(user.ApplicationRoleId);
                    IdentityResult roleResult = await userManager.AddToRoleAsync(useraa, approle.Name);
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }

            return RedirectToAction("Index");


        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpPost]
        [Route("signinuser")]
        public async Task<IActionResult> signinuserAsync(loginDetails user, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(user.email, user.pass, true,  false);
                if (result.Succeeded)
                {
                    if (returnUrl!=null)
                    {
                        return RedirectToLocal(returnUrl);

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                    return RedirectToAction("Index", new { @error = 1 });

                }
            }
            return RedirectToAction("Index");
        }

        [Route("logoutUSer")]
        public async Task<IActionResult> LogoutUser()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index");

        }


        [Route("run")]
        public IActionResult run()
        {
            return View();
        }
        [Route("runnnn")]

        public Boolean runnn()
        {
            sc.ExecuteExe();
            return true;
        }
        [Route("gettt")]
        public void getUser()
        {
            var d = this.userManager.Users.ToList();
        }
    }
}
