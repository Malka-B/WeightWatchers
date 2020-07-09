using AutoMapper;
using Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Subscriber.Data.Entities;
using Subscriber.Services;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly WeightWatchersContext _weightWatchersContext;
        private readonly IMapper _mapper;

        public SubscriberRepository(WeightWatchersContext weightWatchersContext, IMapper mapper)
        {
            _weightWatchersContext = weightWatchersContext;
            _mapper = mapper;
        }

        public async Task<bool> CardExistAsync(int cardId)
        {
            CardEntity card = await _weightWatchersContext.Card
                .FirstOrDefaultAsync(c => c.Id == cardId);
            if (card != null)
            {
                return true;
            }
            return false;
        }

        public async Task<CardModel> GetCardAsync(int id)
        {
            CardEntity cardEntity = await _weightWatchersContext.Card.
                Include(s => s.Subscriber)
                .FirstOrDefaultAsync(c => c.Id == id);

            CardModel cardModel = _mapper.Map<CardModel>(cardEntity);
            return cardModel;

        }

        public async Task<bool> IsEmailValiAsync(string email)
        {
            SubscriberEntity subscriberEntity = await _weightWatchersContext.Subscriber
                                                .FirstOrDefaultAsync(s => s.Email == email);
            if (subscriberEntity != null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> LoginAsync(string email, string password)
        {
            SubscriberEntity subscriberEntity = await _weightWatchersContext.Subscriber
                                                .FirstOrDefaultAsync(s => s.Email == email
                                                && s.Password == password);

            CardEntity cardEntity = await _weightWatchersContext.Card
                                   .FirstOrDefaultAsync(c => c.SubscriberId == subscriberEntity.Id);
            return cardEntity.Id;
        }

        public async Task<bool> RegisterAsync(SubscriberModel subscriberModel)
        {
            SubscriberEntity subscriberEntity = _mapper.Map<SubscriberEntity>(subscriberModel);

            subscriberEntity.Id = Guid.NewGuid();
            CardEntity cardEntity = new CardEntity
            {
                OpenDate = DateTime.Now,
                BMI = 0,
                Height = subscriberModel.Height,
                UpdateDate = DateTime.Now,
                SubscriberId = subscriberEntity.Id
            };
            
            await _weightWatchersContext.Card.AddAsync(cardEntity);
            await _weightWatchersContext.Subscriber.AddAsync(subscriberEntity);
            await _weightWatchersContext.SaveChangesAsync();
            return true;
        }



        public async Task UpdateBMIAsync(UpdateMeasure message)
        {
            CardEntity card = await _weightWatchersContext.Card
                .FirstOrDefaultAsync(c => c.Id == message.CardId);
            float BMI = ((float)(message.Weight / (((decimal)(card.Height / 100)) * ((decimal)(card.Height / 100)))));
            card.BMI = BMI;
            await _weightWatchersContext.SaveChangesAsync();
        }

        public async Task<bool> ValidateLoginAsync(string email, string password)
        {
            SubscriberEntity subscriber = await _weightWatchersContext.Subscriber
                .FirstOrDefaultAsync(s => s.Email == email
                                       && s.Password == password);
            if (subscriber != null)
            {
                return true;
            }
            return false;
        }
    }
}
