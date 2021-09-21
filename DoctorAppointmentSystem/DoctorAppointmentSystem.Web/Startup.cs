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
using MySqlProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlProject.Data.Migrations;
using MySqlProject.Models;
using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Service;
using HospitalManagement.Core.Repositories;
using HospitalManagement.Core.UnitOfWork;
using HospitalManagement.Data;
using Autofac;
using HospitalManagement.Core;
using Autofac.Extensions.DependencyInjection;

namespace MySqlProject
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
            builder.RegisterModule(new HospitalModule(connectionString, migrationAssemblyName));
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;

            services.AddDbContext<HospitalContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(connectionStringName), b => b.MigrationsAssembly(migrationAssemblyName)));

            //services.AddDbContext<HospitalContext>(options =>
            //   options.UseMySql(
            //       Configuration.GetConnectionString("DefaultConnection")));


            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseMySql(
            //       Configuration.GetConnectionString("DefaultConnection")));
          
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
               .AddDefaultUI()
               .AddEntityFrameworkStores<HospitalContext>()
               .AddDefaultTokenProviders();

            //services.AddScoped<DbContext, HospitalContext>();
            //services.AddScoped<IDoctorService, DoctorService>();
            //services.AddScoped<IDepartmentService, DepartmentService>();
            //services.AddScoped<IDoctorRepository, DoctorRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IHospitalUnitOfWork, HospitalUnitOfWork>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //container = app.ApplicationServices.CreateScope().ServiceProvider;
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

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

                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
