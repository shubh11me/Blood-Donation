using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Pocos
{
    public class loginDetails
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string pass { get; set; }

    }
}
