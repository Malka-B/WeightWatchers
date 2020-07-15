using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Measure.Services.Interfaces;
using Measure.Services.Models;
using Messages.Events;
using NServiceBus;
using System.Net.Mail;
using System.Net;


namespace Measure.Services
{
    public class MeasureService : IMeasureService
    {
        private readonly IMeasureRepository _measureRepository;
       

        public MeasureService(IMeasureRepository measureRepository)
        {
            _measureRepository = measureRepository;           
        }

        public async Task<int> AddMeasureAsync(MeasureModel measureModel)
        {
            measureModel.Date = DateTime.Now;
            measureModel.Status = "InProcess";           
            
            return await _measureRepository.AddMeasureAsync(measureModel);
        }

        public void sendEmail()
        {
            var fromAddress = new MailAddress("weightwatchersmb@gmail.com", "Weight Watchers");
            var toAddress = new MailAddress("mb0583219718@gmail.com", "Aaaa");
            const string fromPassword = "1212121212WW";
            const string subject = "test";
            const string body = "Hey now!! from weight watchers mb";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var messageEmail = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(messageEmail);
            }
            
        }

        public async Task UpdateStatusAsync(SubscriberUpdated message)
        {
            await _measureRepository.UpdateStatusAsync(message);
        }
    }
}
