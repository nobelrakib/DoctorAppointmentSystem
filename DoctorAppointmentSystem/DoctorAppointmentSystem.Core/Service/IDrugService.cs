using DoctorAppointmentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IDrugService
    {
        void AddNewDrug(Drug drug);
        void EditDrug(Drug drug);
        void DeleteDrug(int id);
        Drug GetDrug(int id);
        IEnumerable<Drug> GetDrugs(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered);
    }
}
