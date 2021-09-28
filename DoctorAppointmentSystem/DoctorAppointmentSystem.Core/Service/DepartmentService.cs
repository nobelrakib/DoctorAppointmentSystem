using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public class DepartmentService : IDepartmentService
    {
        private IDoctorAppointmentUnitOfWork _DoctorAppointmentUnitOfWork;
        public DepartmentService(IDoctorAppointmentUnitOfWork DoctorAppointmentUnitOfWork)
        {
            _DoctorAppointmentUnitOfWork = DoctorAppointmentUnitOfWork;
        }
        public void AddNewDepartment(Department department)
        {
            try
            {
                if (department == null || string.IsNullOrWhiteSpace(department.Name))
                    throw new InvalidOperationException("department name is missing");
                _DoctorAppointmentUnitOfWork.DepartmentRepository.Add(department);
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
        public void EditDepartment(Department department)
        {
            try
            {
                var oldDepartment = _DoctorAppointmentUnitOfWork.DepartmentRepository.GetById(department.Id);
                if(oldDepartment == null) throw new InvalidOperationException("department name is missing");

                oldDepartment.Name = department.Name;
                oldDepartment.Description = department.Description;
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
        public void DeleteDepartment(int id)
        {
            try
            {
                var department = _DoctorAppointmentUnitOfWork.DepartmentRepository.GetById(id);
                _DoctorAppointmentUnitOfWork.DepartmentRepository.Remove(department);
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Department GetDepartment(int id)
        {
            return _DoctorAppointmentUnitOfWork.DepartmentRepository.GetById(id);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _DoctorAppointmentUnitOfWork.DepartmentRepository.Get();
        }
        public IEnumerable<Department> GetDepartments(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered)
        {
            var department = _DoctorAppointmentUnitOfWork.DepartmentRepository.Get(
                out total,
                out totalFiltered,
                x => x.Name.Contains(searchText),
                x => x.OrderByDescending(b => b.Id),
                "",
                pageIndex,
                pageSize,
                true);

            return _DoctorAppointmentUnitOfWork.DepartmentRepository.Get(
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
