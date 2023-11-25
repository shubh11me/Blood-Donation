using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Models
{
    public class BloodRequests
    {
        public BloodRequests()
        {
            req_time = DateTime.Now; // Set the default value to the current time
        }
        [Key]
        public int requestId { get; set; }
        [Required]
        public int bloodId { get; set; }
        [Required]

        public string forHospital { get; set; }
        [Required]

        public string byUser { get; set; }
        [DefaultValue("pending")]
        public string req_status { get; set; }
        
        public DateTime req_time { get; set; }

        [ForeignKey("bloodId")]
        public virtual BloodGroups bloodgroup { get; set; }
    }
    public class myRequests
    {
        public int req_id { get; set; }
        public string? req_hospital { get; set; } //when we retreieve for user as it needs name of hosp
        public string? req_user { get; set; }//when we retreieve for hosp as it needs name of user
        public string req_blood_group { get; set; }
        public string req_status { get; set; }
        public DateTime req_date { get; set; }
    }
}
