using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Subscriber.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Subscriber.NSB
{
    public class SubscriberSaga : Saga<SubscriberData>,
        IAmStartedByMessages<UpdateBMI>,
        IHandleMessages<TrackingsUpdated>
    {
        private readonly ISubscriberService _subscriberService;
        static ILog log = LogManager.GetLogger<SubscriberSaga>();
        private SubscriberUpdated subscriber = new SubscriberUpdated();

        public SubscriberSaga(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        public async Task Handle(UpdateBMI message, IMessageHandlerContext context)
        {
            bool isBMIUpdated = await _subscriberService.UpdateBMIAsync(message);
            subscriber.MeasureId = message.MeasureId;
            if (isBMIUpdated)
            {                
                subscriber.Comment = "BMI updated. ";
                Data.IsBMIUpdated = true;
            }
            else
            {
                subscriber.Comment = "update BMI failed. ";
                Data.IsBMIUpdated = false;
            }
            
            await ProcessOrder(context);
        }

        public async Task Handle(TrackingsUpdated message, IMessageHandlerContext context)
        {
            Data.IsTrackingUpdated = true;
            await ProcessOrder(context);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SubscriberData> mapper)
        {
            mapper.ConfigureMapping<UpdateBMI>(message => message.MeasureId)
                .ToSaga(sagaData => sagaData.MeasureId);

            mapper.ConfigureMapping<TrackingsUpdated>(message => message.MeasureId)
                .ToSaga(sagaData => sagaData.MeasureId);
        }        

        private async Task ProcessOrder(IMessageHandlerContext context)
        {
            if (Data.IsBMIUpdated && Data.IsTrackingUpdated)
            {
                subscriber.Status = "Succeeded";
                await context.Publish(subscriber);
                MarkAsComplete();
            }
            if (Data.IsBMIUpdated )
            {
                subscriber.Status = "Succeeded";
                await context.Publish(subscriber);
                MarkAsComplete();
            }
            else 
            {
                subscriber.Status = "Failed";
                await context.Publish(subscriber);
                MarkAsComplete();
            }
        }
    }
}
