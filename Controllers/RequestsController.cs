using Blood_Donation.Helpers;
using Blood_Donation.Identity;
using Blood_Donation.Models;
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
    [Route("/requests")]
    public class RequestsController : Controller
    {
        BloodContext _db;
        userHelper ushelp;
        public RequestsController(BloodContext db, SignInManager<ApplicationUser> s, UserManager<ApplicationUser> u)
        {
            _db = db;
            //signInManager = s;
            //userManager = u;
            ushelp = new userHelper(u, s);
        }
        [Route("requestForMe")]
        [Authorize(Roles = "hospital")]
        public async Task<IActionResult> requestForMe()
        {
            var user = await ushelp.getUserLogged();
            var allrqs = new Helper(_db).GetRequestsForme(user.Id);
            ViewBag.reqs = allrqs;
            ViewBag.cnt = allrqs.Count;
            return View();
        }
    

        [Authorize(Roles="user")]
        public async Task<IActionResult> Index()
        {
            //ushelp.getUserByid(s.req_hospital).Result.fullname
            var user = ushelp.getUserLogged().Result;
            ViewBag.us = user;
            var allrqs = new Helper(_db).GetMyRequests(user.Id);
            ViewBag.reqs = allrqs;
            ViewBag.cnt = allrqs.Count;
            return View();
        }
        [Route("delRequestBlood")]
        [HttpGet]
        public IActionResult delrequestBlood(int req_id,string user)
        {
            try
            {
                var blodreq = _db.BloodRequests.Find(req_id);
                if (blodreq != null && blodreq.byUser==user)
                {
                    _db.Remove(blodreq);
                    _db.SaveChanges();
                    return Ok(new { message = "Record Deleted" });
                }
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { message = "No Blood Request found" });

            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;

                return new JsonResult(new { message = e.Message });
            }

        }

        [Route("updRequestBlood")]
        [HttpGet]
        public IActionResult updrequestBlood(int req_id, string newVal)
        {
            try
            {
                var blodreq = _db.BloodRequests.Find(req_id);
                if (blodreq != null)
                {
                    blodreq.req_status = newVal;
                    _db.Update<BloodRequests>(blodreq);
                    _db.SaveChanges();
                    return Ok(new { message = "Record Updated" });
                }
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult(new { message = "No Blood Request found" });

            }
            catch (Exception e)
            {
                Response.StatusCode = StatusCodes.Status204NoContent;

                return new JsonResult(new { message = e.Message });
            }

        }


        [Route("requestBlood")]
        [HttpPost]
        public IActionResult requestBlood( [FromForm] BloodRequests blds)
        {
            try
            {
                var blod = _db.BloodGroups.Where(bl => bl.id == blds.bloodId).FirstOrDefault();
                if (blod!=null)
                {
                    _db.BloodRequests.Add(blds);
                    _db.SaveChanges();
                    Response.StatusCode = StatusCodes.Status200OK;
                    return new JsonResult(new { message = "Blood Requested" });
                }
                else
                {
                    Response.StatusCode = StatusCodes.Status404NotFound;
                    return new JsonResult(new { message = "No Blood Group Found" });
                }
            }catch(Exception e)
            {
                Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return new JsonResult(new { message = e.Message });
            }
        }
    }
}
