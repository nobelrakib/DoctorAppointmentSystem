using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using DoctorAppointmentSystem.Core.Models;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IDynamodbAppointmentService
    {
        Task<List<Document>> All(string paginationToken = "");
        //Task<IEnumerable<DynamodbAppointmentModel>> Find(SearchRequest searchReq);
        Task Add(DynamodbAppointmentModel entity);
        Task Remove(Guid readerId);
    }
}
