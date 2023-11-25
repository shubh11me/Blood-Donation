using Blood_Donation.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blood_Donation.Helpers
{
    public class userHelper
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public userHelper(UserManager<ApplicationUser> u, SignInManager<ApplicationUser> s)
        {
            userManager = u;
            signInManager = s;
        }

        

        public async Task<ApplicationUser> getUserLogged()
        {
            //var b = await userManager.GetRolesAsync(a);
            var userId = signInManager.Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var a = await userManager.FindByIdAsync(userId);
            return a;

        }
        public async Task<ApplicationUser> getUserByid(string id)
        {
            return await userManager.FindByIdAsync(id);
        }
    }
}
