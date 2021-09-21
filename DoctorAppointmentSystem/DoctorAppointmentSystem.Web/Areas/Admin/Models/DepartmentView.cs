using Autofac;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DoctorAppointmentSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class DepartmentView
    {
        private IDepartmentService _departmentService;
        private IApplicationBuilder _applicationBuilder;
        public DepartmentView(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public DepartmentView()
        {
            _departmentService = Startup.AutofacContainer.Resolve<IDepartmentService>();
        }
        public object GetDepartments(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;

            var records = _departmentService.GetDepartments(
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
                                record.Description,
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        public void Delete(int id)
        {
            _departmentService.DeleteDepartment(id);
        }
    }
}
