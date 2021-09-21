using Autofac;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class DrugUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile file { get; set; }
        public string ImageName { get; set; }

        private IDrugService _drugService;
        private FileService _fileService;
        public DrugUpdateModel()
        {

            _drugService = Startup.AutofacContainer.Resolve<IDrugService>();
            _fileService = Startup.AutofacContainer.Resolve<FileService>();
        }
        public DrugUpdateModel(IDrugService drugService, FileService fileService)
        {
            _drugService = drugService;
            _fileService = fileService;
        }

        public void AddDrug()
        {
            try
            {
                SaveFile();
                _drugService.AddNewDrug(new Drug
                {
                    Name = this.Name,
                    Description = this.Description,
                    ImageName = _fileService.FileName
                    // ImageName=this.
                });
                Notification = new NotificationModel("Success !", "Drug Added Successfully", NotificationModel.NotificationType.Success);

            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed !!", "Failed to Insert Drug", NotificationModel.NotificationType.Fail);
            }
        }
        public void Load(int id)
        {
            var drug = _drugService.GetDrug(id);
            if (drug != null)
            {
                Id = drug.Id;
                Name = drug.Name;
                Description = drug.Description;
                ImageName = drug.ImageName;
            }

        }

        public void Edit()
        {
            try
            {
                SaveFile();
                var drug = new Drug
                {
                    Id = Id,
                    Name = Name,
                    Description = Description,
                    ImageName = ImageName,
                };
                _drugService.EditDrug(drug);
                Notification = new NotificationModel("Success !", "drug Added Successfully", NotificationModel.NotificationType.Success);
            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed !!", "Failed to Update drug", NotificationModel.NotificationType.Fail);
            }
        }
        public void SaveFile()
        {
            _fileService.SaveFile(file);
            ImageName = file.FileName;
        }
    }
}
