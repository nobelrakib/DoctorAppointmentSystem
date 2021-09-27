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
    
    public class AppointmentController : Controller
    {
        private readonly INotyfService _notyf;
        public AppointmentController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult Index()
        {
            var model = new AppointmentViewModel();
            return View(model);
        }
        [Authorize(Roles = "Admin,Doctor")]
        public IActionResult GetAppointments()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new AppointmentViewModel();
            var data = model.GetAppointments(tableModel);
            return Json(data);
        }
       [Authorize(Roles = "Admin,Doctor,Patient")]
      // [AllowAnonymous]
        public async Task< IActionResult >Chamber(int id)
        {
            var model = new AppointmentViewModel();
            await model.AssignPermisssion(id);
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public JsonResult EmailSending(string url)
        {
            var model = new EmailSendingModel();
            model.SendEmail(url);
            _notyf.Success("Success Notification");
            return Json(""); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task< IActionResult> Delete(int id)
        {
            var model = new AppointmentViewModel();
            await model.Delete(id);
            return LocalRedirect("/Admin/Appointment/Index");
        }
    }
}
