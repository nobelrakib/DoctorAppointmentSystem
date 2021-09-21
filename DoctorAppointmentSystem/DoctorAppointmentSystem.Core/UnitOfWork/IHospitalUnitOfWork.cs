using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Repositories;
using DoctorAppointmentSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.UnitOfWork
{
    public interface IHospitalUnitOfWork : IUnitOfWork<HospitalContext>
    {
       IDoctorRepository DoctorRepository { get; set; }
       IDepartmentRepository DepartmentRepository { get; set; }
       IDrugRepository DrugRepository { get; set; }
    }
}
