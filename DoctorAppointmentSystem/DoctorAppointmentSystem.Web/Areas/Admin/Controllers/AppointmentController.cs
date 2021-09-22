using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            var model = new AppointmentViewModel();
            return View(model);
        }
        public IActionResult GetAppointments()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new AppointmentViewModel();
            var data = model.GetAppointments(tableModel);
            return Json(data);
        }

        public IActionResult Chamber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new AppointmentViewModel();
            model.Delete(id);
            return LocalRedirect("/Admin/Appointment/Index");
        }
    }
}
