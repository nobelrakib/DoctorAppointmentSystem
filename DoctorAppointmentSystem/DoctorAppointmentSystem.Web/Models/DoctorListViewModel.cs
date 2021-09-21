using DoctorAppointmentSystem.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Models
{
    public class DoctorListModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentDepartment { get; set; }

    }
}
