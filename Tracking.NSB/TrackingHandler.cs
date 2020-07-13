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
    public class TrackingHandler : IHandleMessages<MeasureAdded>
    {
        private readonly ITrackingService _trackingService;
        public TrackingHandler(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }
        public async Task Handle(MeasureAdded message, IMessageHandlerContext context)
        {
            TrackingModel trackingModel = new TrackingModel()
            {
                BMI = (message.Weight) / 10,
                Weight = message.Weight,
                Comments = "gfgfgf",
                trend = 5
            };
            await _trackingService.AddTracikngAsync(trackingModel);
        }
    }
}
