using Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

namespace Tracking.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _trackingRepository;
        public TrackingService(ITrackingRepository trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }

        public async Task<bool> AddTracikngAsync(TrackingModel trackingModel)
        {
            trackingModel.Date = DateTime.Now;

            return await _trackingRepository.AddTrackingAsync(trackingModel);
        }

        public async Task<List<TrackingModel>> GetTrackingsAsync(Paginator paginator)
        {
            return await _trackingRepository.GetTrackingsAsync(paginator);
        }
    }
}
