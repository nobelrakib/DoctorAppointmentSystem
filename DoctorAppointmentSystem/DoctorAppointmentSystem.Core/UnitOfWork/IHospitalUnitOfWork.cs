using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Repositories;
using HospitalManagement.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.UnitOfWork
{
    public interface IHospitalUnitOfWork : IUnitOfWork<HospitalContext>
    {
       IDoctorRepository DoctorRepository { get; set; }
       IDepartmentRepository DepartmentRepository { get; set; }
       IDrugRepository DrugRepository { get; set; }
    }
}
