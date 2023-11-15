using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Models
{
    public class BloodGroups
    {
        [Key]
        public int id { get; set; }
        public string blood_group_name { get; set; }

    }
}
