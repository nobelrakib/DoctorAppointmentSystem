using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public class DepartmentRepository : Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
