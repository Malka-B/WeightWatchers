using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messages.Events;
using NServiceBus;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;

namespace Measure.WepApi
{
    public class TrackingHandler : IHandleMessages<MeasureAdded>
    {
        private readonly ITrackingService _trackingService;
        private readonly IMapper _mapper;

        public TrackingHandler(ITrackingService trackingService, IMapper mapper)
        {
            _trackingService = trackingService;
            _mapper = mapper;
        }
        public async Task Handle(MeasureAdded message, IMessageHandlerContext context)
        {
            TrackingModel trackingModel = new TrackingModel()
            {
                BMI = (message.Weight) / 10,
                Date = DateTime.Now,
                trend = -2,
                Weight = message.Weight,
                Comments = "adding a measure"
            };
            await _trackingService.AddTracikngAsync(trackingModel);
            TrackingsUpdated trackingsUpdated = new TrackingsUpdated()
            {
                MeasureId = message.MeasureId,
                succeeded = true
            };
            await context.Publish(trackingsUpdated)
                .ConfigureAwait(false);
        }
    }
}
