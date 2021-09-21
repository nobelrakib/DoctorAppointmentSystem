using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public class DrugRepository :Repository<Drug>, IDrugRepository
    {
        public DrugRepository(DbContext dbContext)
           : base(dbContext)
        {

        }
    }
}
