using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace DoctorAppointmentSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        public HomeController(ILogger<HomeController> logger, INotyfService notyf)
        {
            _logger = logger;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult AppointmentAdd()
        {
            var model = new AppointmentUpdateModel();
            model.InitiateDoctor();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public  IActionResult AppointmentAdd(AppointmentUpdateModel model)
        {
            try
            {
                model.Add();
                model.InitiateDoctor();
                _notyf.Success("Success Notification");
            }
            catch(Exception ex)
            {
                _notyf.Warning(ex.Message);
            };
           
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
