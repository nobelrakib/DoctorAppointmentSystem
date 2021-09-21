using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
    }
}
