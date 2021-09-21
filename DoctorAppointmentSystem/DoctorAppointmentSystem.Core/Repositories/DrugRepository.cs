using HospitalManagement.Core.Entities;
using HospitalManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Repositories
{
    public class DrugRepository :Repository<Drug>, IDrugRepository
    {
        public DrugRepository(DbContext dbContext)
           : base(dbContext)
        {

        }
    }
}
