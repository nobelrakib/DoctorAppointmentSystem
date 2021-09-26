using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Seeding
{
    public class Seed
    {
        public static async Task Initialize(
           DoctorAppointmentContext context,
           UserManager<ExtendedIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();


            string roleAdmin = "Admin", roleDoctor = "Doctor", rolePatient = "Patient";

            string UserName = "admin@gmail.com", password = "1abcdeA@";
            string email = "admin@gmail.com", name = "admin";
            if (await roleManager.FindByNameAsync(roleAdmin) == null)
                await roleManager.CreateAsync(new IdentityRole(roleAdmin));

            if (await roleManager.FindByNameAsync(roleDoctor) == null)
                await roleManager.CreateAsync(new IdentityRole(roleDoctor));

            if (await roleManager.FindByNameAsync(rolePatient) == null)
                await roleManager.CreateAsync(new IdentityRole(rolePatient));

            if (await userManager.FindByNameAsync(UserName) == null)
            {
                var user = new ExtendedIdentityUser
                {
                    Name = name,
                    UserName = UserName,
                    Email = email,
                    PhoneNumber = "01777121212"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, roleAdmin);
                }
                _ = user.Id;
            }

        }
    }
}
