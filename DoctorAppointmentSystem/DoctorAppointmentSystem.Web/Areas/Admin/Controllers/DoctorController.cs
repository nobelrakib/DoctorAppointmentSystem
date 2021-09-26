using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        public FileService fileService;
        public DoctorController(FileService fileService)
        {
            this.fileService = fileService;
        }
        //public IActionResult Index() => View();

        //[HttpPost]
        //public IActionResult Index(IFormFile file)
        //{
        //    fileService.SaveFile(file);
        //    var x = fileService.FileName;
        //    return View();
        //}
        public IActionResult Index()
        {
            var model = new DoctorViewModel();
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new DoctorUpdateModel();
            model.GetAllDepartment();
            model.GetAllUsers();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DoctorUpdateModel model)
        {
            await model.AddDoctor();
            model.GetAllDepartment();
            model.GetAllUsers();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new DoctorUpdateModel();
            model.GetAllDepartment();
            model.GetAllUsers();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DoctorUpdateModel model)
        {
            model.Edit();
            model.GetAllDepartment();
            model.GetAllUsers();
            return View(model);
        }
        public IActionResult GetDoctors()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new DoctorViewModel();
            var data = model.GetDoctors(tableModel);
            return Json(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new DoctorViewModel();
            await model.Delete(id);
            return LocalRedirect("/Admin/Doctor/Index");
        }
    }
}
