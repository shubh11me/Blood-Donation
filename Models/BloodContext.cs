using Blood_Donation.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blood_Donation.Models;
using Microsoft.AspNetCore.Identity;

namespace Blood_Donation.Models
{
    public class BloodContext:IdentityDbContext<ApplicationUser, ApplicationRoles, string>  
    {
        public BloodContext(DbContextOptions<BloodContext> options):base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<BloodGroups> BloodGroups { get; set; }
        //public DbSet<Blood_Donation.Models.UserViewModel> UserViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Ignore<IdentityUserLogin<string>>();
            //builder.Ignore<IdentityUserRole<string>>();

            //builder.Ignore<IdentityUserClaim<string>>();
            //builder.Ignore<IdentityUserToken<string>>();
            //builder.Ignore<IdentityUser<string>>();
            //builder.Ignore<ApplicationUser>();
            builder.Entity<IdentityUserLogin<string>>().HasKey(s => s.UserId);
            //builder.Entity<IdentityUserRole<string>>().HasKey(s => s.RoleId);
            builder.Entity<IdentityUserRole<string>>()
       .HasKey(e => new { e.UserId, e.RoleId });
            builder.Entity<IdentityUserClaim<string>>().HasKey(s => s.Id);
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
            builder.Entity<IdentityUser>().HasKey(s => s.Id);

            builder.Entity<BloodGroups>().HasData(
            new BloodGroups() {id=1, blood_group_name = "A+" },
            new BloodGroups() { id = 2, blood_group_name = "B+" },
            new BloodGroups() { id = 3, blood_group_name = "A-" },
            new BloodGroups() { id = 4, blood_group_name = "B-" },
            new BloodGroups() { id = 5, blood_group_name = "O+" },
            new BloodGroups() { id = 6, blood_group_name = "O-" },
            new BloodGroups() { id = 7, blood_group_name = "AB" }
            );
        }
    }

}
