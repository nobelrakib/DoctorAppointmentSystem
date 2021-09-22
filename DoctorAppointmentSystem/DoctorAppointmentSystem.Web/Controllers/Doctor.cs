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
        public DoctorAppointmentContext DoctorAppointmentContext;
        public int pageSize = 3;
        public Doctor(DoctorAppointmentContext DoctorAppointmentContext)
        {
            this.DoctorAppointmentContext = DoctorAppointmentContext;
        }
        public IActionResult Index(string department, int pageNumber = 1)
        {
            string page = HttpContext.Request.Query["department"];
            return View(new DoctorListModel
            {
                Doctors = DoctorAppointmentContext.Doctors
                  .Where(p => department == null || p.Department.Name == department)
                    .OrderBy(d => d.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pageNumber,
                    ItemsPerPage = pageSize,
                    TotalItems = department == null ?
                         DoctorAppointmentContext.Doctors.Count() :
                         DoctorAppointmentContext.Doctors.Where(e =>
                             e.Department.Name == department).Count()
                },
                CurrentDepartment=department
            });
        }
    }
}
