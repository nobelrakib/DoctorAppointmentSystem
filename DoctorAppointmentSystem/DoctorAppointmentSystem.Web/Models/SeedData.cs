using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Entities;
using HospitalManagement.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlProject.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            HospitalContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<HospitalContext>();
            var service = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IDepartmentService>();
           
            context.Doctors.AddRange(
                    new Doctor
                    {
                        Name = "neimar",
                        Description = "A boat for one person",
                        Designation="Professor",
                        DepartmentId=2,
                        //Department=service.GetDepartment(9),
                        ImageName="Joy.jpg",
                    },
                       new Doctor
                       {
                           Name = "ronaldo",
                           Description = "A boat for one person",
                           Designation = "Professor",
                           DepartmentId = 2,
                          // Department = service.GetDepartment(9),
                           ImageName = "nabik.jpg",
                       },
                       new Doctor
                       {
                           Name = "messi",
                           Description = "A boat for one person",
                           Designation = "Professor",
                           DepartmentId = 2,
                           //Department = service.GetDepartment(9),
                           ImageName = "abbas.jpg",
                       },
                          new Doctor
                          {
                              Name = "mbappe",
                              Description = "A boat for one person",
                              Designation = "Professor",
                             DepartmentId = 2,
                            //  Department = service.GetDepartment(9),
                              ImageName = "nakib.jpg",
                          }
                          


                );
                context.SaveChanges();
            
        }
    }
}
