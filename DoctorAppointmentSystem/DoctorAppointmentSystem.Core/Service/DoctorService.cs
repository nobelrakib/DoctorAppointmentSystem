using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public class DoctorService : IDoctorService
    {
        private IDoctorAppointmentUnitOfWork _DoctorAppointmentUnitOfWork;
        public DoctorService(IDoctorAppointmentUnitOfWork DoctorAppointmentUnitOfWork)
        {
            _DoctorAppointmentUnitOfWork = DoctorAppointmentUnitOfWork;
        }
        public void AddNewDoctor(Doctor doctor)
        {
            try
            {
                if (doctor == null || string.IsNullOrWhiteSpace(doctor.Name))
                    throw new InvalidOperationException("doctor name is missing");
                _DoctorAppointmentUnitOfWork.DoctorRepository.Add(doctor);
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EditDoctor(Doctor doctor)
        {
            try
            {
                var oldDoctor = _DoctorAppointmentUnitOfWork.DoctorRepository.GetById(doctor.Id);
                oldDoctor.Name = doctor.Name;
                oldDoctor.ImageName = doctor.ImageName;
                oldDoctor.DepartmentId = doctor.DepartmentId;
                oldDoctor.Description = doctor.Description;
                oldDoctor.UserId = doctor.UserId;
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteDoctor(int id)
        {
            try
            {
                var doctor = _DoctorAppointmentUnitOfWork.DoctorRepository.GetById(id);
                _DoctorAppointmentUnitOfWork.DoctorRepository.Remove(doctor);
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Doctor GetDoctor(int id)
        {
            return _DoctorAppointmentUnitOfWork.DoctorRepository.GetById(id);
        }
        public Doctor GetDoctorByUserId(string id)
        {
            return _DoctorAppointmentUnitOfWork.DoctorRepository.GetDoctorByUserId(id);
        }
        public IEnumerable<Doctor> GetDoctors(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered)
        {
            return _DoctorAppointmentUnitOfWork.DoctorRepository.Get(
                out total,
                out totalFiltered,
                x => x.Name.Contains(searchText),
                x => x.OrderByDescending(b => b.Id),
                "",
                pageIndex,
                pageSize,
                true);
        }

    }
}
