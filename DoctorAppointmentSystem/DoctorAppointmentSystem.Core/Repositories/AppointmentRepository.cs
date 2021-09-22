using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
