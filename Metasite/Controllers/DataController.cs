using System.Collections.Generic;
using Metasite.DAL;
using Metasite.DAL.Dtos;
using Metasite.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Metasite.Controllers
{

    public class DataController : Controller
    {
        private readonly IDataRepository _dataRepository;
        public DataController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("api/data/eventtypes")]
        public IEnumerable<EventTypeDto> GetEventTypes()
        {
            return _dataRepository.GetEventTypes();
        }

        [HttpGet]
        public IEnumerable<EventsCountDto> Get()
        {
            return _dataRepository.GetEventsCountByType(1);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
