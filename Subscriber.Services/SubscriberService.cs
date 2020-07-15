using AutoMapper;
using Messages.Commands;
using Messages.Events;
using NServiceBus;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Subscriber.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IMapper _mapper;

        public SubscriberService(ISubscriberRepository subscriberRepository, IMapper mapper)
        {
            _subscriberRepository = subscriberRepository;
            _mapper = mapper;
        }

        public async Task<CardModel> GetCardAsync(int id)
        {
            bool isCardExist = await _subscriberRepository.CardExistAsync(id);
            if (isCardExist)
            {
                return await _subscriberRepository.GetCardAsync(id);
            }
            return null;
        }



        public async Task<int> LoginAsync(string email, string password)
        {
            bool isLoginValidate = await _subscriberRepository.ValidateLoginAsync(email, password);
            if (isLoginValidate)
            {
                return await _subscriberRepository.LoginAsync(email, password);
            }
            return -1;
        }

        public async Task<bool> RegisterAsync(SubscriberModel subscriberModel)
        {
            
            bool isEmailValid = await _subscriberRepository.IsEmailValiAsync(subscriberModel.Email);
            if (isEmailValid)
            {
                return await _subscriberRepository.RegisterAsync(subscriberModel);
            }
            return false;
        }

        public async Task<bool> CardExistAsync(int cardId)
        {
            return await _subscriberRepository.CardExistAsync(cardId);
        }

        public async Task<float> UpdateBMIAsync(MeasureAdded message)
        {
            bool isCardExist = await _subscriberRepository.CardExistAsync(message.CardId);
            if (isCardExist)
            {
                var bmi = await _subscriberRepository.UpdateBMIAsync(message);
                return bmi;
            }
            return -1;
        }
    }
}
