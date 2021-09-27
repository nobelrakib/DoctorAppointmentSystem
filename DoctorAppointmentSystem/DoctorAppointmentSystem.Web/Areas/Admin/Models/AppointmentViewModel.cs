using Autofac;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class AppointmentViewModel
    {
        private IAppointmentService _appointmentService;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IDoctorService _doctorService;

        public AppointmentViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public AppointmentViewModel()
        {
            _appointmentService = Startup.AutofacContainer.Resolve<IAppointmentService>();
            _userManager = Startup.AutofacContainer.Resolve<UserManager<ExtendedIdentityUser>>();
            _roleManager = Startup.AutofacContainer.Resolve<RoleManager<IdentityRole>>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _doctorService = Startup.AutofacContainer.Resolve<IDoctorService>();

        }
        public object GetAppointments(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctorId = _doctorService.GetDoctorByUserId(userId).Id;
            var records = _appointmentService.GetAppointments(
                doctorId,
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                out total,
                out totalFiltered);

            return new
            {
                recordsTotal = total,
                recordsFiltered = totalFiltered,
                data = (from record in records
                        select new string[]
                        {
                                record.Name,
                                record.Symptoms,
                                record.Date.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }
        public async Task AssignPermisssion(int id)
        {
            var appointmentEmail = _appointmentService.GetAppointment(id).Email;
            var patient = await _userManager.FindByEmailAsync(appointmentEmail);
            if (!(await _userManager.IsInRoleAsync(patient,"Patient")))
            {
                await _userManager.AddToRoleAsync(patient, "Patient");
            }
        }
        public async Task Delete(int id)
        {
            var appointmentEmail = _appointmentService.GetAppointment(id).Email;
            var patient = await _userManager.FindByEmailAsync(appointmentEmail);
            var roles = await _userManager.GetRolesAsync(patient);
            await _userManager.RemoveFromRolesAsync(patient, roles);
            _appointmentService.DeleteAppointment(id);
        }
    }
}
