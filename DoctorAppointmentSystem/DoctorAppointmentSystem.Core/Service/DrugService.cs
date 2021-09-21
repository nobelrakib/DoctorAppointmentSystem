using HospitalManagement.Core.Entities;
using HospitalManagement.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HospitalManagement.Core.Service
{
    public class DrugService : IDrugService
    {
        private IHospitalUnitOfWork _hospitalUnitOfWork;
        public DrugService(IHospitalUnitOfWork hospitalUnitOfWork)
        {
            _hospitalUnitOfWork = hospitalUnitOfWork;
        }
        public void AddNewDrug(Drug drug)
        {
            try
            {
                if (drug == null || string.IsNullOrWhiteSpace(drug.Name))
                    throw new InvalidOperationException("drug name is missing");
                _hospitalUnitOfWork.DrugRepository.Add(drug);
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
        public void EditDrug(Drug drug)
        {
            try
            {
                var olddrug = _hospitalUnitOfWork.DrugRepository.GetById(drug.Id);
                olddrug.Name = drug.Name;
                olddrug.ImageName = drug.ImageName;
                olddrug.Description = drug.Description;
                _hospitalUnitOfWork.Save();
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
                var drug = _hospitalUnitOfWork.DrugRepository.GetById(id);
                _hospitalUnitOfWork.DrugRepository.Remove(drug);
                _hospitalUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Drug GetDrug(int id)
        {
            return _hospitalUnitOfWork.DrugRepository.GetById(id);
        }
        public IEnumerable<Drug> GetDrugs(
            int pageIndex,
            int pageSize,
            string searchText,
            out int total,
            out int totalFiltered)
        {
            return _hospitalUnitOfWork.DrugRepository.Get(
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
