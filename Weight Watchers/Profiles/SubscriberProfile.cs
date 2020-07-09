using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Subscriber.Data.Entities;
using Subscriber.Services.Models;
using Weight_Watchers.DTO;

namespace Subscriber.Data.Profiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<SubscriberModel, SubscriberDTO>();
            CreateMap<SubscriberDTO, SubscriberModel>();
            CreateMap<SubscriberEntity, SubscriberModel>();
            CreateMap<SubscriberModel, SubscriberEntity>();
            CreateMap<CardModel, CardDTO>();
            CreateMap<CardDTO, CardModel>();
            CreateMap<CardEntity, CardModel>()
                .ForMember(destination => destination.FirstName, option => option.MapFrom(src =>
                src.Subscriber.FirstName))
                .ForMember(destination => destination.LastName, option => option.MapFrom(src =>
                 src.Subscriber.LastName));
            CreateMap<CardModel, SubscriberEntity>();
           
        }
    }
}
