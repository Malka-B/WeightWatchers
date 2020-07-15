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
        IAmStartedByMessages<MeasureAdded>,
        IHandleMessages<TrackingsUpdated>
    {
        private readonly ISubscriberService _subscriberService;
        static ILog log = LogManager.GetLogger<SubscriberSaga>();
        static SubscriberUpdated subscriber = new SubscriberUpdated();

        public SubscriberSaga(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }
        public async Task Handle(MeasureAdded message, IMessageHandlerContext context)
        {
            float isBMIUpdated = await _subscriberService.UpdateBMIAsync(message);

            log.Info($"in Subscriber Saga, got measure/ id: {message.MeasureId}");
           
            if (isBMIUpdated != -1)
            {                
                subscriber.Comment = "BMI updated. ";
                Data.IsBMIUpdated = true;
                BMIUpdated bMIUpdated = new BMIUpdated
                {
                    MeasureId = message.MeasureId,
                    BMI = isBMIUpdated,
                    CardId = message.CardId,
                    Weight = message.Weight
                };
                await context.Publish(bMIUpdated);
            }
            else
            {
                subscriber.Comment = "update BMI failed. ";
                //send to error queue
                Data.IsBMIUpdated = false;
            }
            
            await ProcessOrder(context);
        }

        public async Task Handle(TrackingsUpdated message, IMessageHandlerContext context)
        {
            subscriber.MeasureId = message.MeasureId;
            if (message.succeeded)
            {
                subscriber.Comment += "Tracking added";
                Data.IsTrackingUpdated = true;
            }
            else
            {
                subscriber.Comment += "Tracking failed";
                //send to error q and rollback
                Data.IsTrackingUpdated = false;
            }
            await ProcessOrder(context);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SubscriberData> mapper)
        {
            mapper.ConfigureMapping<MeasureAdded>(message => message.MeasureId)
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
            //else(Data.IsBMIUpdated && Data.IsTrackingUpdated == false || Data.IsBMIUpdated== false && Data.IsTrackingUpdated)
            //{
            //    subscriber.Status = "Failed";
            //    await context.Publish(subscriber);
            //    MarkAsComplete();
            //}
            //if (Data.IsBMIUpdated )
            //{
            //    subscriber.Status = "Succeeded";
            //    await context.Publish(subscriber);
            //    MarkAsComplete();
            //}
            //else 
            //{
            //    subscriber.Status = "Failed";
            //    await context.Publish(subscriber);
            //    MarkAsComplete();
            //}
        }
    }
}
