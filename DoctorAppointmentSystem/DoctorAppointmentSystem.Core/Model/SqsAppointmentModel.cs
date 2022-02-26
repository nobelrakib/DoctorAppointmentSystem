using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Model
{
    public class SqsAppointmentModel
    {
        
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
