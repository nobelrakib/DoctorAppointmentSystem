using Autofac;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class DrugViewModel
    {
        private IDrugService _drugService;
        public DrugViewModel(IDrugService drugService)
        {
            _drugService = drugService;
        }
        public DrugViewModel()
        {
            _drugService = Startup.AutofacContainer.Resolve<IDrugService>();
        }
        public object GetDrugs(DataTablesAjaxRequestModel tableModel)
        {
            int total = 0;
            int totalFiltered = 0;

            var records = _drugService.GetDrugs(
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
                                record.Description,
                                record.Id.ToString()
                        }
                    ).ToArray()

            };
        }

        public void Delete(int id)
        {
            _drugService.DeleteDrug(id);
        }
    }
}
