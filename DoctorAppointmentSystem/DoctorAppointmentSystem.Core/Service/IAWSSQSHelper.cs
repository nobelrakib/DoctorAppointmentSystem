using Amazon.SQS.Model;
using DoctorAppointmentSystem.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Core.Service
{
    public interface IAWSSQSHelper
    {
        Task<bool> SendMessageAsync(SqsAppointmentModel SqsAppointmentModel);
        Task<List<Message>> ReceiveMessageAsync();
    }
}
