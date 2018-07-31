using System.Collections.Generic;
using System.Linq;
using Metasite.DAL.Dtos;
using Metasite.DAL.Interfaces;

namespace Metasite.DAL.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly MSContext _context;

        public DataRepository(MSContext context)
        {
            _context = context;
        }

        public List<EventsCountDto> GetEventsCountByType(int eventTypeId)
        {
            var result = (from e in _context.EventLogs
                          where e.EventTypeId == eventTypeId
                          group e by e.EventTypeId into g
                          select new EventsCountDto
                          {
                              EventType = g.Select(a => a.EventType.Type).FirstOrDefault(),
                              EventsCount = g.Count()
                          }).ToList();
            return result;
        }

        public List<EventTypeDto> GetEventTypes()
        {
            return _context.EventTypes.Select(a => new EventTypeDto {Type = a.Type, EventTypeId = a.EventTypeId}).ToList();
        }
    }
}
