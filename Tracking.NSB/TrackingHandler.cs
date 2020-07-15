using Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Entities;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;

namespace Tracking.NSB
{
    public class TrackingHandler : IHandleMessages<BMIUpdated>
    {

        private readonly ITrackingService _trackingService;

        public TrackingHandler(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        public async Task Handle(BMIUpdated message, IMessageHandlerContext context)
        {
            TrackingsUpdated trackingsUpdated = new TrackingsUpdated();

            TrackingModel trackingModel = new TrackingModel()
            {
                BMI = message.BMI,
                Weight = message.Weight,
                trend = (int)(message.Weight - message.BMI),
                Comments = "you are great!",
                CardId = message.CardId

            };
            
            var isTrackingAdded = await _trackingService.AddTracikngAsync(trackingModel);
            if (isTrackingAdded)
            {
                trackingsUpdated.MeasureId = message.MeasureId;
                trackingsUpdated.succeeded = true;
            }
            else
            {
                trackingsUpdated.MeasureId = message.MeasureId;
                trackingsUpdated.succeeded = false;
            }

            await context.Publish(trackingsUpdated)
                .ConfigureAwait(false);
        }
    }
}
