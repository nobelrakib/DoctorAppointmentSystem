using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public IList<Appointment> Appointments { get; set; }
    }
}
