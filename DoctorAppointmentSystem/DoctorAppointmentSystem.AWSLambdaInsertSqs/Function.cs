using System;
using System.IO;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.DynamoDBEvents;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using DoctorAppointmentSystem.Core.Service;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DoctorAppointmentSystem.AWSLambdaInsertSqs
{
    public class Function
    {
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {

            var dynamodbService = new DynamodbAppointmentService();
            var documentList = await dynamodbService.All();
            var appointmenttList = new List<Appointment>();
            foreach(var document in documentList)
            {
                var appointment = new Appointment();
                foreach (var attribute in document.GetAttributeNames())
                {
                    string stringValue = null;
                    var value = document[attribute];
                    if(attribute == "email") appointment.EmailAddress = value;
                    if (attribute == "id") appointment.Id =(Guid) value;
                    if (attribute == "appointmentDate") appointment.AppointmentDate =(DateTime)value;

                }
                appointmenttList.Add(appointment);
            }

            return new APIGatewayProxyResponse
            {
                StatusCode =200,
                Body=JsonConvert.SerializeObject(appointmenttList)
            };
        }
    }
    public class Appointment
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}