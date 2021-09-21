using Autofac;
using HospitalManagement.Core.Contexts;
using HospitalManagement.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlProject.Models
{
    public class AppointmentUpdateModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Symptoms { get; set; }
        public Doctor Doctor { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public int DoctorId { get; set; }
        public Gender Gender { get; set; }
        public DateTime Date { get; set; }
        public HospitalContext context;
        public AppointmentUpdateModel()
        {
            context= Startup.AutofacContainer.Resolve<HospitalContext>();
        }
        public void Add()
        {
            context.Appointment.Add(new Appointment()
            {
                Name=Name,
                Email=Email,
                PhoneNumber=PhoneNumber,
                Symptoms=Symptoms,
                DoctorId=DoctorId,
                Gender=Gender,
                Date=Date
            });
            context.SaveChanges();
        }
        public void InitiateDoctor()
        {
            Doctors = context.Doctors.AsEnumerable();
        }
    }
}
