using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorAppointmentSystem.Core.Model
{
    public class AllMessage
    {
        public AllMessage()
        {
            SqsAppointmentModel = new SqsAppointmentModel();
        }
        public string MessageId { get; set; }
        public string ReceiptHandle { get; set; }
        public SqsAppointmentModel SqsAppointmentModel { get; set; }
    }
}
