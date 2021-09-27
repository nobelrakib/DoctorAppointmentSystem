using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorAppointmentSystem.Core.Repositories
{
    public class DoctorRepository : Repository<Doctor>,IDoctorRepository
    {
        public DoctorAppointmentContext _context;
        public DoctorRepository(DoctorAppointmentContext dbContext)
           : base(dbContext)
        {
            _context = dbContext;
        }

        public Doctor GetDoctorByUserId(string id)
        {
            return _context.Doctors.Where(x => x.UserId == id).FirstOrDefault();
        }
    }
}
