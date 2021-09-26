using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Entities
{
    public class ExtendedIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public Doctor Doctor { get; set; }
    }
}
