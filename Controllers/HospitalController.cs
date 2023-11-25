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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blood_Donation.Controllers
{

    public class HospitalController : Controller
    {
        UserManager<ApplicationUser> userManager;
        RoleManager<ApplicationRoles> roleManager;
        SignInManager<ApplicationUser> signinmanager;
        BloodContext _db;
        public HospitalController(UserManager<ApplicationUser> u, RoleManager<ApplicationRoles> r, SignInManager<ApplicationUser> s, BloodContext db)
        {
            userManager = u;
            roleManager = r;
            signinmanager = s;
            _db = db;
        }


        [Authorize(Roles = "hospital")]
        public async Task<IActionResult> Index()
        {
            ViewBag.msg = null;

            IEnumerable<BloodGroups> bloods = _db.BloodGroups;
            ViewBag.bloods = bloods.Select(a => new SelectListItem()
            {
                Text = a.blood_group_name,
                Value = a.id.ToString()
            });
            ViewBag.hospital = await userManager.GetUserAsync(signinmanager.Context.User);
            var bllods = getMyBloodStock();
            ViewBag.bloodstocks = bllods;
            return View();
        }

        [HttpPut]
        public IActionResult UpdateBloodQuan(int bloodStockId, int neqQuantity)
        {
            try
            {
                var blodStock = _db.BloodGroupStocks.Find(bloodStockId);
                if (blodStock != null)
                {
                    blodStock.bloodStockQuantity = neqQuantity;
                    _db.Update(blodStock);
                    _db.SaveChanges();
                    return Ok(new { message = "Updated Success" });
                }
                Response.StatusCode = StatusCodes.Status404NotFound;

                return new JsonResult(new { message = "No Blood Stock found" });
            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;

                return new JsonResult(new { message = e.Message });

            }
        }
        [HttpDelete]
        public IActionResult DeleteBloodQuan(int bloodStockId)
        {
            try
            {
                var blodStock = _db.BloodGroupStocks.Find(bloodStockId);
                if (blodStock != null)
                {
                    _db.Remove(blodStock);
                    _db.SaveChanges();
                    return Ok(new { message = "Record Deleted" });
                }
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { message = "No Blood Stock found" });

            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;

                return new JsonResult(new { message = e.Message });
            }

        }
        [Authorize(Roles = "hospital")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(BloodStockAdd blo)
        {
            IEnumerable<BloodGroups> bloods = _db.BloodGroups;
            ViewBag.bloods = bloods.Select(a => new SelectListItem()
            {
                Text = a.blood_group_name,
                Value = a.id.ToString()
            });
            var bllods = getMyBloodStock();
            ViewBag.bloodstocks = bllods;

            ViewBag.hospital = await userManager.GetUserAsync(signinmanager.Context.User);
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

                var isAlready = bllods.Where(a => a.bloodStockBlood == blo.bloodStockBlood).FirstOrDefault();
                if (isAlready!=null)
                {
                    _db.BloodGroupStocks.Update(new BloodGroupStock() { id=isAlready.id,bloodStockQuantity=(blo.bloodStockQuantity+isAlready.bloodStockQuantity),bloodStockHospital= userId ,bloodStockBlood=isAlready.bloodStockBlood});
                    _db.SaveChanges();
                    ViewBag.msg = new msgPasser() { error = false, msg = "Blood Group stock Updated" };
                    return View();
                }

                BloodGroupStock b = new BloodGroupStock()
                {
                    bloodStockBlood = blo.bloodStockBlood,
                    bloodStockHospital = userId,
                    bloodStockQuantity = blo.bloodStockQuantity
                };

                _db.BloodGroupStocks.Add(b);
                _db.SaveChanges();
                ViewBag.msg = new msgPasser() { error = false, msg = "Blood Group stock added" };
                return View();

            }
            catch (Exception e)
            {
                ViewBag.msg = new msgPasser() { error = true, msg = e.Message };
                return View();
            }

        }
        public IEnumerable<BloodGroupStockwithName> getMyBloodStock()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bl = _db.BloodGroupStocks.Where(blood => blood.bloodStockHospital == userId).Select(s =>
                 new BloodGroupStockwithName()
                 {
                     bloodGroupName = s.bloodGroups.blood_group_name,
                     bloodStockQuantity = s.bloodStockQuantity,
                     bloodStockBlood = s.bloodStockBlood,
                     id = s.id
                 }
                );
            return bl;
        }

        public IActionResult Signup(int? error)
        {
            ViewBag.errors = null;
            ViewBag.errorcode = null;
            if (error == 1)
            {
                @ViewBag.errorcode = "Invalid Credentials";

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> signin(loginDetails login)
        {
            var user = await signinmanager.PasswordSignInAsync(login.email, login.pass, true, false);
            if (user.Succeeded)
            {
                return RedirectToAction("index", "Hospital");
            }
            return RedirectToAction("Signup", "Hospital", new { error = 1 });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(HospitalViewModel hospD)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser u = new ApplicationUser()
                    {
                        fullname = hospD.hospital_name,
                        UserName = hospD.hospital_email,
                        Email = hospD.hospital_email
                    };
                    IdentityResult hospuser = await userManager.CreateAsync(u, hospD.hospital_conf_password);
                    if (hospuser.Succeeded)
                    {
                        var role = await roleManager.FindByIdAsync("2");
                        if (role == null)
                        {
                            ViewBag.errors = "Please register with real role";
                            await userManager.DeleteAsync(u);
                            return View();
                        }
                        IdentityResult rolefe = await userManager.AddToRoleAsync(u, role.Name);
                        if (rolefe.Succeeded)
                        {
                            ViewBag.errors = "Please Login Now";

                            return View();
                        }
                    }
                    return View();
                }
                else
                {
                    ViewBag.errors = "Please register with real role";


                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.errors = e.Message;


                return View();
            }
        }
    }
}
