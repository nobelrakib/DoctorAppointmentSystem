using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DrugController : Controller
    {
        public FileService fileService;
        public DrugController(FileService fileService)
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
            var model = new DrugViewModel();
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new DrugUpdateModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(DrugUpdateModel model)
        {
            model.AddDrug();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new DrugUpdateModel();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DrugUpdateModel model)
        {
            model.Edit();
            return View(model);
        }
        public IActionResult GetDrugs()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new DrugViewModel();
            var data = model.GetDrugs(tableModel);
            return Json(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new DrugViewModel();
            model.Delete(id);
            return LocalRedirect("/Admin/Drug/Index");
        }
    }
}
