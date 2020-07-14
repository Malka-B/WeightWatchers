using Messages.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

namespace Tracking.Services.Interfaces
{
    public interface ITrackingService
    {
        Task<bool> AddTracikngAsync(TrackingModel trackingModel);
        Task<List<TrackingModel>> GetTrackingsAsync(Paginator paginator);

    }
}
