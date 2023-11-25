using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Pocos
{
    public class BloodStockAdd
    {
        [Required]
        public int bloodStockBlood { get; set; }
        [Required]

        public int bloodStockQuantity { get; set; }
    }

    public class BloodGroupStockwithName
    {
        public int id { get; set; }
        public int bloodStockBlood { get; set; }

        public int bloodStockQuantity { get; set; }
  

        public string bloodGroupName { get; set; }
    }
}
public class hospitalNameID
{
    public string hospital { get; set; }
    public string hospitalId { get; set; }
    public string hospitalEmail { get; set; }
    public string hospitalAdd { get; set; }
    public List<BloodGroupStockwithHospital> stocks { get; set; }

}

public class BloodGroupStockwithHospital
{
    //public string hospital { get; set; }
    public int bloodid { get; set; }
    //public string hospitalEMail { get; set; }
    public int bloodQuantity { get; set; }
    public string bloodname { get; set; }
    public string reqStatus { get; set; }

}
