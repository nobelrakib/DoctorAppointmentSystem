using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagement.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlProject.Areas.Admin.Models;
using MySqlProject.Models;

namespace MySqlProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(DoctorUpdateModel model)
        {
            model.AddDoctor();
            model.GetAllDepartment();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new DoctorUpdateModel();
            model.GetAllDepartment();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DoctorUpdateModel model)
        {
            model.Edit();
            model.GetAllDepartment();
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
        public IActionResult Delete(int id)
        {
            var model = new DoctorViewModel();
            model.Delete(id);
            return LocalRedirect("/Admin/Department/Index");
        }
    }
}
