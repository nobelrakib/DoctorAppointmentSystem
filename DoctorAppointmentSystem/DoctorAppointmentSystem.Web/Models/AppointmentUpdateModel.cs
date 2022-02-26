using Autofac;
using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorAppointmentSystem.Core.Models;
namespace DoctorAppointmentSystem.Web.Models
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
        public DoctorAppointmentContext context;
        public IDynamodbAppointmentService dynamodbAppointmentService;
        public AppointmentUpdateModel()
        {
            context= Startup.AutofacContainer.Resolve<DoctorAppointmentContext>();
            dynamodbAppointmentService = Startup.AutofacContainer.Resolve<IDynamodbAppointmentService>();

        }
        public void Add()
        {
            context.Appointment.Add(new Appointment()
            {
                Name = Name,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Symptoms = Symptoms,
                DoctorId = DoctorId,
                Gender = Gender,
                Date = Date
            });
            //await dynamodbAppointmentService.Add(new DynamodbAppointmentModel
            //{
            //    AppointmentDate = Date,
            //    EmailAddress = Email,
            //    Id = Guid.NewGuid()
            //});

            context.SaveChanges();
        }
        public void InitiateDoctor()
        {
            Doctors = context.Doctors.AsEnumerable();
        }
    }
}
