using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Repositories;
using HospitalManagement.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.UnitOfWork
{
    public class HospitalUnitOfWork : UnitOfWork<HospitalContext>,IHospitalUnitOfWork
    {
        public IDoctorRepository DoctorRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IDrugRepository DrugRepository { get; set; }
        public HospitalUnitOfWork(string connectionString, string migrationAssemblyName) : base(connectionString, migrationAssemblyName)
        {
            DepartmentRepository = new DepartmentRepository(_dbContext);
            DrugRepository = new DrugRepository(_dbContext);
            DoctorRepository = new DoctorRepository(_dbContext);
        }
    }
}
