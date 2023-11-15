using Blood_Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Helpers
{
    public class Helper: IHelper
    {
        public string IdMaker(string prefix,string id)
        {
            return prefix + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + id + new Random().Next().ToString();
        }
        public Boolean isUserPresent(string email, BloodContext db)
        {
            var s=db.Users.FirstOrDefault(e => e.email == email);
            if (s == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
