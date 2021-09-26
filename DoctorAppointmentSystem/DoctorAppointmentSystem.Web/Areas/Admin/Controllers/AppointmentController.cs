using AspNetCoreHero.ToastNotification.Abstractions;
using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Doctor,Patient")]
    public class AppointmentController : Controller
    {
        private readonly INotyfService _notyf;
        public AppointmentController(INotyfService notyf)
        {
            _notyf = notyf;
        }
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
        public JsonResult EmailSending(string url)
        {
            var model = new EmailSendingModel();
            model.SendEmail(url);
            _notyf.Success("Success Notification");
            return Json(""); 
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
