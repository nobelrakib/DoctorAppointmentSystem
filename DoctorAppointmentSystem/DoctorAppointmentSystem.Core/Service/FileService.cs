using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace DoctorAppointmentSystem.Core.Service
{
    public class FileService
    {
        private IWebHostEnvironment _env;
        public string FileName { get; set; }
        private readonly IAWSS3BucketHelper _AWSS3BucketHelper;
        public FileService(IWebHostEnvironment env, IAWSS3BucketHelper AWSS3BucketHelper)
        {
            _env = env;
           _AWSS3BucketHelper = AWSS3BucketHelper;

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

        public async Task<bool> UploadFileS3Bucket(IFormFile uploadFileName)
        {
            try
            {
                var path = Path.Combine(_env.WebRootPath + "\\FrontEnd\\images", FileName);
                using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
                {

                    return await _AWSS3BucketHelper.UploadFile(fsSource, FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string RandomName(string prefix = "") =>
            $"img{prefix}_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.png";
    }
}
