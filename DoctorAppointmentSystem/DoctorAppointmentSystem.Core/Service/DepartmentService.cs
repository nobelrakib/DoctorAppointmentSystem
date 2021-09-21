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
        private IHospitalUnitOfWork _hospitalUnitOfWork;
        public DepartmentService(IHospitalUnitOfWork hospitalUnitOfWork)
        {
            _hospitalUnitOfWork = hospitalUnitOfWork;
        }
        public void AddNewDepartment(Department department)
        {
            try
            {
                if (department == null || string.IsNullOrWhiteSpace(department.Name))
                    throw new InvalidOperationException("department name is missing");
                _hospitalUnitOfWork.DepartmentRepository.Add(department);
                _hospitalUnitOfWork.Save();
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
                var oldDepartment = _hospitalUnitOfWork.DepartmentRepository.GetById(department.Id);
                oldDepartment.Name = department.Name;
                oldDepartment.Description = department.Description;
                _hospitalUnitOfWork.Save();
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
                var department = _hospitalUnitOfWork.DepartmentRepository.GetById(id);
                _hospitalUnitOfWork.DepartmentRepository.Remove(department);
                _hospitalUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Department GetDepartment(int id)
        {
            return _hospitalUnitOfWork.DepartmentRepository.GetById(id);
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _hospitalUnitOfWork.DepartmentRepository.Get();
        }
        public IEnumerable<Department> GetDepartments(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered)
        {
            return _hospitalUnitOfWork.DepartmentRepository.Get(
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
