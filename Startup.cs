using Blood_Donation.Identity;
using Blood_Donation.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BloodContext>(item => item.UseSqlServer(Configuration.GetConnectionString("dbwala")));
            services.AddIdentity<ApplicationUser, ApplicationRoles>()
            .AddEntityFrameworkStores<BloodContext>()
            .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(s =>
            {
                if (s.ClaimsIssuer == "user")
                {
                    s.LoginPath = "/user/signin";
                    s.AccessDeniedPath = "/user/signin";
                }
                else if(s.ClaimsIssuer == "hospital")
                {
                    s.LoginPath = "/hospital/signup";
                    s.AccessDeniedPath = "/hospital/signup";
                }
                else
                {
                    s.LoginPath = "/user/signin";

                }
            }
                );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages(async context => {
                if (context.HttpContext.Response.StatusCode == 403)
                {
                    var b = context.HttpContext.Request.Query;
                    // your redirect
                }
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
