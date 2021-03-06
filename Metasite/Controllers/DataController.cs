﻿using System.Collections.Generic;
using Metasite.Repositories.Dtos;
using Metasite.Repositories.Interfaces;
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

        [HttpPost]
        [Route("api/data/eventlog")]
        public IEnumerable<EventLogDto> Get([FromBody] FilterDto filter)
        {
            return _dataRepository.GetEventLog(filter);
        }

        [HttpPost]
        [Route("api/data/eventtops")]
        public IEnumerable<EventTopDto> GetTops([FromBody] FilterDto filter)
        {
            return _dataRepository.GetTops(filter);
        }
    }
}
