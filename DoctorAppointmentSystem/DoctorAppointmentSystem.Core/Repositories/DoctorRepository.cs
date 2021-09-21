using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public class DoctorRepository : Repository<Doctor>,IDoctorRepository
    {
        public DoctorRepository(DbContext dbContext)
           : base(dbContext)
        {

        }
    }
}
