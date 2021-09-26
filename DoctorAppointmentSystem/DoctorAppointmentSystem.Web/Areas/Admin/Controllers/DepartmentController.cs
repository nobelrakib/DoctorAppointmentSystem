using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Mvc;
using DoctorAppointmentSystem.Web.Areas.Admin.Models;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        //  public IDepartmentService ds;
        public IActionResult Index()
        {
            var model = new DepartmentView();
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new DepartmentUpdateModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(DepartmentUpdateModel model)
        {
            model.AddDepartment();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new DepartmentUpdateModel();
            model.Load(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentUpdateModel model)
        {
            model.Edit();
            return View(model);
        }
        public IActionResult GetDepatments()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = new DepartmentView();
            var data = model.GetDepartments(tableModel);
            return Json(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new DepartmentView();
            model.Delete(id);
            return LocalRedirect("/Admin/Department/Index");
        }

    }
}
