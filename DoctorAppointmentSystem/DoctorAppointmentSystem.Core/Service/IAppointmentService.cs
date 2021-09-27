using DoctorAppointmentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAppointments(
            int doctorId,
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered);
        void DeleteAppointment(int id);
        Appointment GetAppointment(int id);
    }
}
