using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Entities;
using HospitalManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Repositories
{
    public class DoctorRepository : Repository<Doctor>,IDoctorRepository
    {
        public DoctorRepository(DbContext dbContext)
           : base(dbContext)
        {

        }
    }
}
