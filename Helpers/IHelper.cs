using Blood_Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Helpers
{
    public interface IHelper
    {
        public string IdMaker(string prefix, string id);
        public Boolean isUserPresent(string email, BloodContext db);

    }
}
