using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public DoctorAppointmentContext context;
        public NavigationMenuViewComponent(DoctorAppointmentContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedDepartment = RouteData?.Values["department"];
            return View(context.Doctors
                .Select(x => x.Department.Name)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
