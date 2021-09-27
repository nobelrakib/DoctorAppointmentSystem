using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public class AppointmentService : IAppointmentService
    {
        private IDoctorAppointmentUnitOfWork _DoctorAppointmentUnitOfWork;
        public AppointmentService(IDoctorAppointmentUnitOfWork DoctorAppointmentUnitOfWork)
        {
            _DoctorAppointmentUnitOfWork = DoctorAppointmentUnitOfWork;
        }
        public IEnumerable<Appointment> GetAppointments(
           int doctorId,
           int pageIndex,
           int pageSize,
           string searchText,
           out int total,
           out int totalFiltered)
        {
            return _DoctorAppointmentUnitOfWork.AppointmentRepository.Get(
                out total,
                out totalFiltered,
                x => x.Name.Contains(searchText) && x.DoctorId==doctorId,
                x => x.OrderByDescending(b => b.Id),
                "",
                pageIndex,
                pageSize,
                true);
        }

        public void DeleteAppointment(int id)
        {
            try
            {
                var department = _DoctorAppointmentUnitOfWork.AppointmentRepository.GetById(id);
                _DoctorAppointmentUnitOfWork.AppointmentRepository.Remove(department);
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Appointment GetAppointment(int id)
        {
            return _DoctorAppointmentUnitOfWork.AppointmentRepository.GetById(id);
        }
    }
}
