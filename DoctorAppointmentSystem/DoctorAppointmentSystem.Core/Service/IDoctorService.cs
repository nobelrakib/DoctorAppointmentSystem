using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Entities;
using HospitalManagement.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Service
{
    public interface IDoctorService
    {
        void AddNewDoctor(Doctor doctor);
        void EditDoctor(Doctor doctor);
        void DeleteDoctor(int id);
        Doctor GetDoctor(int id);
        IEnumerable<Doctor> GetDoctors(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered);
    }
}
