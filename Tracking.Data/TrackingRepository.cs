using AutoMapper;
using Messages.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Entities;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

namespace Tracking.Data
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly TrackingContext _trackingContext;
        private readonly IMapper _mapper;

        public TrackingRepository(TrackingContext trackingContext, IMapper mapper)
        {
            _trackingContext = trackingContext;
            _mapper = mapper;
        }

        public async Task<bool> AddTrackingAsync(TrackingModel trackingModel)
        {
            TrackingEntity trackingEntity = _mapper.Map<TrackingEntity>(trackingModel);
            await _trackingContext.Trackings.AddAsync(trackingEntity);
            await _trackingContext.SaveChangesAsync();
            TrackingEntity tracking = await _trackingContext.Trackings
                .FirstOrDefaultAsync(t => t.BMI == trackingEntity.BMI
                && t.Comments == trackingEntity.Comments
                && t.Date == trackingEntity.Date);
            return true;
        }

        public async Task<List<TrackingModel>> GetTrackingsAsync(Paginator paginator)
        {
            List<TrackingEntity> trackingEntities = await _trackingContext.Trackings.ToListAsync<TrackingEntity>();
            List<TrackingModel> trackingModels=new List<TrackingModel>();
            for(int i=0;i<trackingEntities.Count;i++)
            {
                trackingModels.Add(_mapper.Map<TrackingModel>(trackingEntities[i]));
            }
            return trackingModels;
        }
    }
}
