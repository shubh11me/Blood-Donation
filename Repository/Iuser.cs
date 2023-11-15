using Blood_Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Repository
{
    public interface Iuser
    {
        public List<Users> getAllUSers();
        public Users getUSerByid(int id);
    }
}
