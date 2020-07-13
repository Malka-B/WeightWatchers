using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracking.Api.DTO;
using Tracking.Data.Entities;
using Tracking.Services.Models;

namespace Tracking.Api.Profiles
{
    public class TrackingProfile : Profile
    {
        public TrackingProfile()
        {
            CreateMap<TrackingModel, TrackingDTO>();
            CreateMap<TrackingDTO, TrackingModel>();
            CreateMap<TrackingModel, TrackingEntity>();
            CreateMap<TrackingEntity, TrackingModel>();
        }
    }
}
