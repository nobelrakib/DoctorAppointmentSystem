using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Repositories;
using DoctorAppointmentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.UnitOfWork
{
    public class DoctorAppointmentUnitOfWork : UnitOfWork<DoctorAppointmentContext>,IDoctorAppointmentUnitOfWork
    {
        public IDoctorRepository DoctorRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IDrugRepository DrugRepository { get; set; }
        public IAppointmentRepository AppointmentRepository { get; set; }
        public DoctorAppointmentUnitOfWork(string connectionString, string migrationAssemblyName) : base(connectionString, migrationAssemblyName)
        {
            DepartmentRepository = new DepartmentRepository(_dbContext);
            DrugRepository = new DrugRepository(_dbContext);
            DoctorRepository = new DoctorRepository(_dbContext);
            AppointmentRepository = new AppointmentRepository(_dbContext);
        }
    }
}
