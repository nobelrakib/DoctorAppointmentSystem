using Autofac;
using DoctorAppointmentSystem.Core.Service;
using DoctorAppointmentSystem.Web.EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointmentSystem.Web.Areas.Admin.Models
{
    public class EmailSendingModel
    {
        public IConfiguration Configuration { get; set; }
        private IAppointmentService _appointmentService;
        public EmailSendingModel()
        {
            Configuration = Startup.AutofacContainer.Resolve<IConfiguration>(); //config;
            _appointmentService = Startup.AutofacContainer.Resolve<IAppointmentService>();
        }
        public void SendEmail(string url)
        {
            var msg = new MailMessage();
            // var url = "https://"+"localhost:44319/admin/Appointment/Chamber";
            string[] urlSepration = url.Split("/");
            msg.Body = $"<a href={url}> click here</a>";
            msg.Subject = "Warning";
            msg.SenderName = "Rakib Hasan";
            msg.Sender = "nobelrakib03@gmail.com";
            msg.Receiver = _appointmentService.GetAppointment(Int32.Parse(urlSepration[6])).Email;
            using (var mailsender = new MailSender(Configuration))
            {
                mailsender.Send(msg);
            }
        }
    }
}
