using Blood_Donation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Helpers
{
    public class Helper : IHelper
    {
        BloodContext _db;
        public Helper(BloodContext db)
        {
            _db = db;
        }
        public string IdMaker(string prefix, string id)
        {
            return prefix + DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + id + new Random().Next().ToString();
        }
        public Boolean isUserPresent(string email, BloodContext db)
        {
            var s = db.Users.FirstOrDefault(e => e.email == email);
            if (s == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public List<myRequests> GetMyRequests(string userId)
        {
            List<myRequests> allRs = new List<myRequests>();
           return _db.BloodRequests.Where(rs => rs.byUser == userId).Select(blr=>new myRequests() { 
           req_blood_group=blr.bloodgroup.blood_group_name,
           req_date=blr.req_time,
           req_hospital=blr.forHospital,
               req_status = blr.req_status,
           req_id=blr.requestId
           }).ToList();
        }
        public List<myRequests> GetRequestsForme(string userId)
        {
            List<myRequests> allRs = new List<myRequests>();
            return _db.BloodRequests.Where(rs => rs.forHospital == userId && rs.req_status!="accepted").Select(blr => new myRequests()
            {
                req_blood_group = blr.bloodgroup.blood_group_name,
                req_date = blr.req_time,
                req_user = blr.byUser,
                req_status = blr.req_status,
                req_id = blr.requestId
            }).ToList();
        }
        public string reqStatus(string user, int blood, string hosp)
        {
            var req = _db.BloodRequests.Where(bl => bl.byUser == user && bl.bloodId == blood && bl.forHospital == hosp).FirstOrDefault();
            if (req == null)
            {
                return "";
            }
            else
            {
                return req.req_status;
            }
        }
    }
}
