using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Models
{
    public class Users
    {
        public int id { get; set; }
        public string user_id { get; set; }
        [Required]
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Range(5,70,ErrorMessage ="Kam Jyad")]
        public int age { get; set; }
        [Required]
        public Gender gender { get; set; }
        [Required,MaxLength(10,ErrorMessage ="Jyada hoagaya")]
        public string pass { get; set; }
        [Required]
        public string isMarried { get; set; }
    }
    public enum Gender {male,female }

}
