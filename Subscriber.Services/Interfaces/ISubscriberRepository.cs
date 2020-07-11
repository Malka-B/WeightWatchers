using Messages.Commands;
using Subscriber.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Services
{
    public interface ISubscriberRepository
    {
        Task<bool> RegisterAsync(SubscriberModel register);
        Task<CardModel> GetCardAsync(int id); 
        Task<int> LoginAsync(string email, string password);
        Task UpdateBMIAsync(UpdateBMI message);
        Task<bool> CardExistAsync(int cardId);
        Task<bool> ValidateLoginAsync(string email, string password);
        Task<bool> IsEmailValiAsync(string email);
    }
}
