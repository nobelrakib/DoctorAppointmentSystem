using Autofac;
using HospitalManagement.Core.Entities;
using HospitalManagement.Core.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlProject.Areas.Admin.Models
{
    public class DoctorUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public IFormFile file { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        private IDoctorService _doctorService;
        private IDepartmentService _departmentService;
        private IServiceProvider _serviceProvider;
        private FileService _fileService;
        public DoctorUpdateModel()
        {

            _doctorService = Startup.AutofacContainer.Resolve<IDoctorService>();
            _departmentService = Startup.AutofacContainer.Resolve<IDepartmentService>();
            _fileService = Startup.AutofacContainer.Resolve<FileService>();
        }
        public DoctorUpdateModel(IDoctorService doctorService,IDepartmentService departmentService,FileService fileService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _fileService = fileService;
        }

        public void AddDoctor()
        {
            try
            {
                SaveFile();
                _doctorService.AddNewDoctor(new Doctor
                {
                    Name = this.Name,
                    Description = this.Description,
                    DepartmentId = this.DepartmentId,
                    ImageName = _fileService.FileName
                    // ImageName=this.
                });
                Notification = new NotificationModel("Success !", "Doctor Added Successfully", NotificationModel.NotificationType.Success);

            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed !!", "Failed to Insert Doctor", NotificationModel.NotificationType.Fail);
            }
        }
        public void Load(int id)
        {
            var doctor = _doctorService.GetDoctor(id);
            if (doctor != null)
            {
                Id = doctor.Id;
                Name = doctor.Name;
                ImageName = doctor.ImageName;
                DepartmentId = doctor.DepartmentId;
                this.Description = doctor.Description;
            }

        }

        public void Edit()
        {
            try
            {
                SaveFile();
                var doctor = new Doctor
                {
                    Id = Id,
                    Name = Name,
                    DepartmentId=DepartmentId,
                    Description = Description,
                    ImageName=ImageName,
                };
                _doctorService.EditDoctor(doctor);
                Notification = new NotificationModel("Success !", "doctor Added Successfully", NotificationModel.NotificationType.Success);
            }
            catch (Exception ex)
            {
                Notification = new NotificationModel("Failed !!", "Failed to Update doctor", NotificationModel.NotificationType.Fail);
            }
        }
        public void GetAllDepartment() =>Departments = _departmentService.GetDepartments();
        public void SaveFile()
        {
            _fileService.SaveFile(file);
            ImageName = file.FileName;
        }
    }
}
