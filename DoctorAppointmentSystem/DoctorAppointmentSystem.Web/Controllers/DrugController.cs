using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Contexts;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Web.Models;

namespace DoctorAppointmentSystem.Web.Controllers
{
    public class DrugController : Controller
    {
        public DoctorAppointmentContext DoctorAppointmentContext;
        public DrugController(DoctorAppointmentContext DoctorAppointmentContext)
        {
            this.DoctorAppointmentContext = DoctorAppointmentContext;
        }
        public IActionResult Index()
        {
            return View(new DrugListViewModel
            {
                Drugs = DoctorAppointmentContext.Drugs.AsEnumerable()

            }); 
        }
    }
}
