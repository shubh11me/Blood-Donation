using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Models
{
    public class HospitalViewModel
    {
        public string hospital_name { get; set; }
        public string hospital_address { get; set; }
        public string hospital_email { get; set; }
        public string hospital_password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string hospital_conf_password { get; set; }
    }
}
