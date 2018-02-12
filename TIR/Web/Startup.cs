using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Data;
using Web.Models;
using Web.Services;

namespace Web
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
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IndexHub, IndexHub>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSignalR();

            services.AddMvc();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolesManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            

            app.UseAuthentication();

            ConfigureSeedData(userManager, rolesManager);

            app.UseSignalR(routes =>
            {
                routes.MapHub<IndexHub>("hub");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void ConfigureSeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> rolesManager)
        {
            if(!rolesManager.RoleExistsAsync("Admin").Result)
            {
                var dupa = rolesManager.CreateAsync(new IdentityRole() {
                    Name = "Admin"
                }).Result;
            }
            if (!rolesManager.RoleExistsAsync("TechnicalUser").Result)
            {
                var dupa = rolesManager.CreateAsync(new IdentityRole()
                {
                    Name = "TechnicalUser"
                }).Result;
            }
            if (!rolesManager.RoleExistsAsync("Room").Result)
            {
                var dupa = rolesManager.CreateAsync(new IdentityRole()
                {
                    Name = "Room"
                }).Result;
            }

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser
                {
                    Email = "admin@azkel.org",
                    UserName = "admin",
                };
                // Hopefully you don't expect that I will provide real password on GitHub public project, do you?
                var dupa2 = userManager.CreateAsync(user, "password").Result;

                if (dupa2.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
