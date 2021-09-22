using Autofac;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class AppointmentViewModel
    {
        private IAppointmentService _appointmentService;
        public AppointmentViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public AppointmentViewModel()
        {
            _appointmentService = Startup.AutofacContainer.Resolve<IAppointmentService>();
        }
        public object GetAppointments(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;

            var records = _appointmentService.GetAppointments(
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

        public void Delete(int id)
        {
            _appointmentService.DeleteAppointment(id);
        }
    }
}
