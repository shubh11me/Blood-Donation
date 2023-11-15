using Blood_Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Repository
{
    public class UserRepository:Iuser
    {
        List<Users> data = new List<Users> {
         new Users{id=1,name="Shubham Todkar",email="shubham@gmail.com",age=21,pass="SHubham"},
         new Users{id=2,name="Aniket Todkar",email="aniket@gmail.com",age=22,pass="Aniket123"},
         new Users{id=3,name="Sayali Pawar",email="sayali@gmail.com",age=21,pass="Sayali987"},
         new Users{id=4,name="Vaishnavi Pawar",email="Vaishnavi@gmail.com",age=23,pass="Vaishnavi789"},
         new Users{id=5,name="Vaishnavi Uagle",email="VaishnaviUgale@gmail.com",age=21,pass="Vaishnavi12345"}
        };

        public List<Users> getAllUSers()
        {
            return data;
        }

        public Users getUSerByid(int id)
        {
            return data.Where(i => i.id == id).FirstOrDefault();
        }

    }
}
