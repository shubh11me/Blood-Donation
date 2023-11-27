using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Controllers
{
    public class MainHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
