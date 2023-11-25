using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Models
{
    public class BloodGroupStock
    {
        public int id { get; set; }
        [Required]
        public int bloodStockBlood { get; set; }
        [Required]

        public int bloodStockQuantity { get; set; }
        [Required]
        
        public string bloodStockHospital { get; set; }

        [ForeignKey("bloodStockBlood")]
        public virtual BloodGroups bloodGroups { get; set; }
    }
}
