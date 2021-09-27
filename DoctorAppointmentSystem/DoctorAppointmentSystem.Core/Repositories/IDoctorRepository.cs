using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        Doctor GetDoctorByUserId(string id);
    }
}
