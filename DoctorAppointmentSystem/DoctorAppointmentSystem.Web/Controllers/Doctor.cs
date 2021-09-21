using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Web.Models;

namespace DoctorAppointmentSystem.Web.Controllers
{
    public class Doctor : Controller
    {
        public HospitalContext hospitalContext;
        public int pageSize = 3;
        public Doctor(HospitalContext hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }
        public IActionResult Index(string department, int pageNumber = 1)
        {
            string page = HttpContext.Request.Query["department"];
            return View(new DoctorListModel
            {
                Doctors = hospitalContext.Doctors
                  .Where(p => department == null || p.Department.Name == department)
                    .OrderBy(d => d.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = department == null ?
                         hospitalContext.Doctors.Count() :
                         hospitalContext.Doctors.Where(e =>
                             e.Department.Name == department).Count()
                },
                CurrentDepartment=department
            });
        }
    }
}
