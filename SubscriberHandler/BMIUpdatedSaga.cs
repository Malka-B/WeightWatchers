using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace SubscriberHandler
{
    public class BMIUpdatedSaga : Saga<BMIUpdatedData>,
        IAmStartedByMessages<BMIUpdated>,
        IAmStartedByMessages<TrackingsUpdated>
    {
        static ILog log = LogManager.GetLogger<BMIUpdatedSaga>();
        SubscriberUpdated subscriber;
        public Task Handle(BMIUpdated message, IMessageHandlerContext context)
        {

            log.Info($"Saga: The measure {message.MeasureId} completed!");
            Data.IsBMIUpdated = true;
            subscriber = new SubscriberUpdated { Status = message.Status, MeasureId = message.MeasureId };
            if (message.Status == "failed")
            {
                subscriber.Comment += "Card Id is wrong";
            }

            return ProcessOrder(context);
        }

        public Task Handle(TrackingsUpdated message, IMessageHandlerContext context)
        {
            Data.IsTrackingUpdated = true;
            if (message.succeeded == false)
            {                
                subscriber.Comment += "Tracking failed";
            }
            return ProcessOrder(context);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BMIUpdatedData> mapper)
        {
            mapper.ConfigureMapping<BMIUpdated>(message => message.MeasureId)
                .ToSaga(sagaData => sagaData.MeasureId);


        }

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsBMIUpdated && Data.IsTrackingUpdated)
            {
                await context.Publish(subscriber);
                MarkAsComplete();
            }
        }
    }
}
