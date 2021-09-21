using HospitalManagement.Core.Entities;
using HospitalManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Repositories
{
    public class DepartmentRepository : Repository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
