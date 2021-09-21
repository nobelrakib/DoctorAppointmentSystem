using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Doctor> Doctor { get; set; }
    }
}
