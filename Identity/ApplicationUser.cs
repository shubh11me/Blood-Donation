using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Identity
{
    public class ApplicationUser: IdentityUser
    {

        public string blood_group { get; set; }
        public string fullname { get;  set; }
    }
}
