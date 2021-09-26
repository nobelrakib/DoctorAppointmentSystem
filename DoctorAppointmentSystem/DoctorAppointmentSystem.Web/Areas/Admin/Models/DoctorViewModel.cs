using Autofac;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Core.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DoctorAppointmentSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DoctorAppointmentSystem.Core.Entities;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class DoctorViewModel
    {
        private IDoctorService _doctorService;
        private IApplicationBuilder _applicationBuilder;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DoctorViewModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public DoctorViewModel()
        {
            _doctorService = Startup.AutofacContainer.Resolve<IDoctorService>();
            _userManager = Startup.AutofacContainer.Resolve<UserManager<ExtendedIdentityUser>>();
            _roleManager = Startup.AutofacContainer.Resolve<RoleManager<IdentityRole>>();
        }
        public object GetDoctors(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;

            var records = _doctorService.GetDoctors(
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
                                "/FrontEnd/images/"+record.ImageName,
                                record.Department?.Name,
                                record.Designation,
                                record.Description,
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        public async Task Delete(int id)
        {
            var userId = _doctorService.GetDoctor(id).UserId;
            var user =  await _userManager.FindByIdAsync(userId);
            var roles = await  _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            _doctorService.DeleteDoctor(id);
        }
    }
}
