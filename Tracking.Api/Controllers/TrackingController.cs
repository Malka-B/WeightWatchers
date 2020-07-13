using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using Tracking.Api.DTO;
using Tracking.Services.Interfaces;
using Tracking.Services.Models;
using Tracking.WebApi.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tracking.Api.Controllers
{
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _trackingService;
        private readonly IMapper _mapper;
        private readonly IMessageSession _messageSession;

        public TrackingController(ITrackingService trackingService, IMapper mapper, IMessageSession messageSession)
        {
            _trackingService = trackingService;
            _mapper = mapper;
            _messageSession = messageSession;
        }
        [HttpGet]
   
        public async Task<List<TrackingDTO>> Get([FromQuery] Paginator paginator = null)
        {
            List<TrackingModel> trackingModels = await _trackingService.GetTrackingsAsync(paginator);
            List<TrackingDTO> trackingDTOs = new List<TrackingDTO>();
            for(int i=0;i<trackingModels.Count;i++)
            {
                trackingDTOs.Add(_mapper.Map<TrackingDTO>(trackingModels[i]));
            }
            return trackingDTOs;
        }

 
    }
}
