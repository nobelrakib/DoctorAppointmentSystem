using DoctorAppointmentSystem.Core.Entities;
using DoctorAppointmentSystem.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctorAppointmentSystem.Core.Service
{
    public class DrugService : IDrugService
    {
        private IDoctorAppointmentUnitOfWork _DoctorAppointmentUnitOfWork;
        public DrugService(IDoctorAppointmentUnitOfWork DoctorAppointmentUnitOfWork)
        {
            _DoctorAppointmentUnitOfWork = DoctorAppointmentUnitOfWork;
        }
        public void AddNewDrug(Drug drug)
        {
            try
            {
                if (drug == null || string.IsNullOrWhiteSpace(drug.Name))
                    throw new InvalidOperationException("drug name is missing");
                _DoctorAppointmentUnitOfWork.DrugRepository.Add(drug);
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
        public void EditDrug(Drug drug)
        {
            try
            {
                var olddrug = _DoctorAppointmentUnitOfWork.DrugRepository.GetById(drug.Id);
                olddrug.Name = drug.Name;
                olddrug.ImageName = drug.ImageName;
                olddrug.Description = drug.Description;
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteDrug(int id)
        {
            try
            {
                var drug = _DoctorAppointmentUnitOfWork.DrugRepository.GetById(id);
                _DoctorAppointmentUnitOfWork.DrugRepository.Remove(drug);
                _DoctorAppointmentUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Drug GetDrug(int id)
        {
            return _DoctorAppointmentUnitOfWork.DrugRepository.GetById(id);
        }
        public IEnumerable<Drug> GetDrugs(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered)
        {
            return _DoctorAppointmentUnitOfWork.DrugRepository.Get(
                out total,
                out totalFiltered,
                x => x.Name.Contains(searchText),
                x => x.OrderByDescending(d => d.Id),
                "",
                pageIndex,
                pageSize,
                true);
        }
    }
}
