using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Core.Models
{
    public class DynamodbAppointmentModel
    {
        [DynamoDBProperty("id")]
        [DynamoDBHashKey]
        public Guid Id { get; set; }

        [DynamoDBProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [DynamoDBProperty("userName")]
        public string Username { get; set; }

        [DynamoDBProperty("addedOn")]
        public DateTime AppointmentDate { get; set; }
    }
}
