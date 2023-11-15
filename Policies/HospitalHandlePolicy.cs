using Blood_Donation.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Policies
{
    public class HospitalHandlePolicy : AuthorizationHandler<IAuthorizationRequirement>
    {
        public SignInManager<ApplicationUser> signinmanager;
        public HospitalHandlePolicy(SignInManager<ApplicationUser> signi)
        {
            signinmanager = signi;

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
        {
            //signinmanager.Context.User.Identity.AuthenticationType;
            var role = context.User.HasClaim(s => s.Value == "hospital"); 
            if (role)
            {   
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
