using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace HospitalManagement.Core.Service
{
    public class FileService
    {
        private IWebHostEnvironment _env;
        public string FileName { get; set; }
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void SaveFile(IFormFile file)
        {
            var name = RandomName();
            var save_path = Path.Combine(_env.WebRootPath + "\\FrontEnd\\images", name);
            FileName = name;
            using (var fileStream = new FileStream(save_path, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
        }

        private string RandomName(string prefix = "") =>
            $"img{prefix}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.png";
    }
}
