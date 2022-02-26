using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.AWSLambdaInsertSqs.Model
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
