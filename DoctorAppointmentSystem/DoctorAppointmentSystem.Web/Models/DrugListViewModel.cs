using DoctorAppointmentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Models
{
    public class DrugListViewModel
    {
        public IEnumerable<Drug> Drugs { get; set; }
    }
}
