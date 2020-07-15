using System;
using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;

namespace Measure.WepApi
{
    public class MeasureHandler : IHandleMessages<SubscriberUpdated>
    {
        private readonly NSB.Services.Interfaces.IMeasureHandlerService _measureHandlerService;

        public MeasureHandler(NSB.Services.Interfaces.IMeasureHandlerService measureHandlerService)
        {
            _measureHandlerService = measureHandlerService;
        }
        public async Task Handle(SubscriberUpdated message, IMessageHandlerContext context)
        {

            

            await _measureHandlerService.UpdateStatusAsync(message);
        }
    }
}
