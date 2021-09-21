using HospitalManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Contexts
{
    public interface IHospitalContext
    {
         DbSet<Doctor> Doctors { get; set; }
         DbSet<Department> Departments { get; set; }
    }
}
