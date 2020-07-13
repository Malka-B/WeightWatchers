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
        // GET: api/<controller>
        [HttpGet]
        //GET /tracking/:cardId? page =<> &size =<>


        public IEnumerable<string> Get([FromQuery] Paginator paginator = null)
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost("measure")]
      

        //public async Task<ActionResult> AddMeasureAsync([FromBody] TrackingDTO trackingDTO)
        //{
        //    //TrackingModel trackingModel = _mapper.Map<TrackingModel>(trackingDTO);
        //    //int measureId = await _measureService.AddMeasureAsync(measureModel);
        //    //MeasureAdded measureAdded = new MeasureAdded
        //    //{
        //    //    MeasureId = measureId,
        //    //    CardId = measureModel.CardId,
        //    //    Weight = measureModel.Weight
        //    //};
        //    //await _messageSession.Publish(measureAdded)
        //    //     .ConfigureAwait(false);

        //    //return Ok("הפעולה נקלטה בהצלחה!");

        //}
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
