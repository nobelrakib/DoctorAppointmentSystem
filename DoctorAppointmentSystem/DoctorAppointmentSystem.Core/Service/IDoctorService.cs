using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IDoctorService
    {
        void AddNewDoctor(Doctor doctor);
        void EditDoctor(Doctor doctor);
        void DeleteDoctor(int id);
        Doctor GetDoctor(int id);
        Doctor GetDoctorByUserId(string id);
        IEnumerable<Doctor> GetDoctors(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered);
    }
}
