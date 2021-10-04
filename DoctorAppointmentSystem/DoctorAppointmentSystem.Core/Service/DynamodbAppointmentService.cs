using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DoctorAppointmentSystem.Core.Constants;
using DoctorAppointmentSystem.Core.Models;


namespace DoctorAppointmentSystem.Core.Service
{
    public class DynamodbAppointmentService : IDynamodbAppointmentService
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly Table _table;
        public DynamodbAppointmentService()
        {
            _client = new AmazonDynamoDBClient();
            _table = Table.LoadTable(_client, AwsConstants.TableName);
        }

        public async Task Add(DynamodbAppointmentModel entity)
        {
            
            try
            {
                var appointment = new Document();
                appointment["id"] = Guid.NewGuid().ToString();
                appointment["email"] = entity.EmailAddress;
                appointment["appointmentDate"] = entity.AppointmentDate;
                await _table.PutItemAsync(appointment);
            }
            catch(Exception ex)
            {

            }
            //  await _context.SaveAsync<DynamodbAppointmentModel>(entity);
        }

        public async Task<List<Document>> All(string paginationToken = "")
        {
            
            ScanFilter scanFilter = new ScanFilter();
            Search search = _table.Scan(scanFilter);
            List<Document> documentList = new List<Document>();
            do
            {
                documentList =await search.GetNextSetAsync();
                
            } while (!search.IsDone);

            return documentList;
        }

        public async Task Remove(Guid id)
         {
            //await _context.DeleteAsync<DynamodbAppointmentModel>(id);

         }

       

        

       
    }
}
