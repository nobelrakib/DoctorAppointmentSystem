using Autofac;
using DoctorAppointmentSystem.Core.Contexts;
using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class DoctorUpdateModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public string UserId { get; set; }

        public IFormFile file { get; set; }
        public string ImageName { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<ExtendedIdentityUser> AppUsers { get; set; }
        public  string BucketImageUrl = "https://doctorappointmentbucket.s3.ap-southeast-1.amazonaws.com/";
        private IDoctorService _doctorService;
        private IDepartmentService _departmentService;
        private IServiceProvider _serviceProvider;
        private FileService _fileService;
        private readonly UserManager<ExtendedIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DoctorAppointmentContext _db;
        private readonly IAWSS3BucketHelper _s3BucketHelper;
        public DoctorUpdateModel()
        {

            _doctorService = Startup.AutofacContainer.Resolve<IDoctorService>();
            _departmentService = Startup.AutofacContainer.Resolve<IDepartmentService>();
            _userManager = Startup.AutofacContainer.Resolve<UserManager<ExtendedIdentityUser>>();
            _fileService = Startup.AutofacContainer.Resolve<FileService>();
            _roleManager = Startup.AutofacContainer.Resolve<RoleManager<IdentityRole>>();
            _db = Startup.AutofacContainer.Resolve<DoctorAppointmentContext>();
            _s3BucketHelper = Startup.AutofacContainer.Resolve<IAWSS3BucketHelper>();

        }
        public DoctorUpdateModel(IDoctorService doctorService,IDepartmentService departmentService,FileService fileService)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _fileService = fileService;
        }

        public async Task AddDoctor()
        {
            try
            {
                SaveFile();
                var user =  await _userManager.FindByIdAsync(UserId);
                var role = "Doctor";
                if(user != null)
                {
                    var roleResult =await _userManager.AddToRoleAsync(user, role);
                }
                _doctorService.AddNewDoctor(new Doctor
                {
                    Name = this.Name,
                    Description = this.Description,
                    DepartmentId = this.DepartmentId,
                    ImageName = _fileService.FileName,
                    UserId=this.UserId
                   // BucketUrl= BucketImageUrl + _fileService.FileName
                    // ImageName=this.
                });
               // await _fileService.UploadFileS3Bucket(file);
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
                UserId = doctor.UserId;
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
                    UserId=UserId,
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
        public void GetAllUsers() => AppUsers = _userManager.Users.ToList();
        public void SaveFile()
        {
            _fileService.SaveFile(file);
            ImageName = _fileService.FileName;
        }
    }
}
