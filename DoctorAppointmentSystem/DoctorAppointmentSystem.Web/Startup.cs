using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using DoctorAppointmentSystem.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DoctorAppointmentSystem.Web.Data.Migrations;
using DoctorAppointmentSystem.Web.Models;
using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Core.Repositories;
using DoctorAppointmentSystem.Core.UnitOfWork;
using DoctorAppointmentSystem.Data;
using Autofac;
using DoctorAppointmentSystem.Core;
using Autofac.Extensions.DependencyInjection;
using AspNetCoreHero.ToastNotification;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Web.Seeding;
using Amazon.S3;

namespace DoctorAppointmentSystem.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IServiceProvider container;
        public static ILifetimeScope AutofacContainer { get; private set; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            builder.RegisterModule(new DoctorAppointmentModule(connectionString, migrationAssemblyName));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            services.AddDbContext<DoctorAppointmentContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(connectionStringName), b => b.MigrationsAssembly(migrationAssemblyName)));

            services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
            services.AddAWSService<IAmazonS3>();
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddIdentity<ExtendedIdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
               //  .AddDefaultUI()
               //.AddRoles<IdentityRole>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<DoctorAppointmentContext>();
               

            //services.AddScoped<DbContext, DoctorAppointmentContext>();
            //services.AddScoped<IDoctorService, DoctorService>();
            //services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddScoped<IDoctorRepository, DoctorRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IDoctorAppointmentUnitOfWork, DoctorAppointmentUnitOfWork>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DoctorAppointmentContext context,
            UserManager<ExtendedIdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            //container = app.ApplicationServices.CreateScope().ServiceProvider;
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            Seed.Initialize(context, userManager, roleManager).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            // SeedData.EnsurePopulated(app);
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<Hubs.WebRTCHub>("/WebRTCHub");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("a",
                    "{department}/Page{pageNumber:int}",                
                    new { Controller = "doctor", action = "Index", pageNumber =1});

                endpoints.MapControllerRoute("b", "Page{pageNumber:int}",
                    new { Controller = "Doctor", action = "Index", pageNumber=1 });

                endpoints.MapControllerRoute("c", "{department}",
                    new { Controller = "doctor", action = "Index", pageNumber=1 });
                endpoints.MapControllerRoute("d",
                    "Doctors/Page{pageNumber}",
                    new { Controller = "Doctor", action = "Index", pageNumber =1});

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<Hubs.WebRTCHub>("/WebRTCHub");
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
