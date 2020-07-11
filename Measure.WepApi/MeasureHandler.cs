﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Measure.Services;
using Measure.Services.Interfaces;
using Messages.Events;
using NServiceBus;

namespace Measure.WepApi
{
    public class MeasureHandler : IHandleMessages<SubscriberUpdated>
    {
        private readonly IMeasureService _measureService;

        public MeasureHandler(IMeasureService measureService)
        {
            _measureService = measureService;
        }
        public async Task Handle(SubscriberUpdated message, IMessageHandlerContext context)
        {
            await _measureService.UpdateStatusAsync(message);
        }
    }
}
