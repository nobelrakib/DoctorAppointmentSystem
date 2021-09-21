using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagement.Core.Contexts;
using Microsoft.AspNetCore.Mvc;
using MySqlProject.Models;

namespace MySqlProject.Controllers
{
    public class DrugController : Controller
    {
        public HospitalContext hospitalContext;
        public DrugController(HospitalContext hospitalContext)
        {
            this.hospitalContext = hospitalContext;
        }
        public IActionResult Index()
        {
            return View(new DrugListViewModel
            {
                Drugs = hospitalContext.Drugs.AsEnumerable()

            }); 
        }
    }
}
