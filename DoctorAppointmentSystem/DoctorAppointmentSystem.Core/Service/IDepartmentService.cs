using DoctorAppointmentSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IDepartmentService
    {
        void AddNewDepartment(Department department);
        void EditDepartment(Department department);
        void DeleteDepartment(int id);
        Department GetDepartment(int id);
        IEnumerable<Department> GetDepartments();
        IEnumerable<Department> GetDepartments(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered);
    }
}
