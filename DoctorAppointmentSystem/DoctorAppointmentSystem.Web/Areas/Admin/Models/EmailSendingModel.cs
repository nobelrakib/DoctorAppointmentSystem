using Autofac;
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
        public EmailSendingModel()
        {
            Configuration = Startup.AutofacContainer.Resolve<IConfiguration>(); //config;
        }
        public void SendEmail(string url)
        {
            var msg = new MailMessage();
           // var url = "https://"+"localhost:44319/admin/Appointment/Chamber";
            msg.Body = $"<a href={url}> click here</a>";
            msg.Subject = "Warning";
            msg.SenderName = "Rakib Hasan";
            msg.Sender = "nobelrakib03@gmail.com";
            msg.Receiver = "nobelrakib03@gmail.com";
            using (var mailsender = new MailSender(Configuration))
            {
                mailsender.Send(msg);
            }
        }
    }
}
