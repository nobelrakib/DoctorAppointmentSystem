using DoctorAppointmentSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Contexts
{
    public interface IDoctorAppointmentContext
    {
         DbSet<Doctor> Doctors { get; set; }
         DbSet<Department> Departments { get; set; }
    }
}
