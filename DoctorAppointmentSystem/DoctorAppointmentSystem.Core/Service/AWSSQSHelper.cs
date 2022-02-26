using Amazon.SQS;
using Amazon.SQS.Model;
using DoctorAppointmentSystem.Core.Constants;
using DoctorAppointmentSystem.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Core.Service
{
    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IAmazonSQS _sqs;
        public AWSSQSHelper(IAmazonSQS sqs)
        {
            this._sqs = sqs;
        }
        public async Task<bool> SendMessageAsync(SqsAppointmentModel SqsAppointmentModel)
        {
            try
            {
                string message = JsonConvert.SerializeObject(SqsAppointmentModel);
                var sendRequest = new SendMessageRequest(AwsConstants.SqsUrl, message);
                // Post message or payload to queue  
                var sendResult = await _sqs.SendMessageAsync(sendRequest);

                return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Message>> ReceiveMessageAsync()
        {
            try
            {
                //Create New instance  
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = AwsConstants.SqsUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };
                //CheckIs there any new message available to process  
                var result = await _sqs.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
