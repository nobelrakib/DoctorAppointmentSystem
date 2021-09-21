using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Symptoms { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public  Gender Gender { get; set; }
        public DateTime Date { get; set; }
    }
}
public enum Gender
{
    Male=1,
    Female
}
