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

        public List<EventLogDto> GetEventLog(FilterDto filter)
        {
            var iqu = _context.EventLogs.AsQueryable();
            if (filter != null)
            {
                if (filter.DateFrom.HasValue)
                    iqu = iqu.Where(a => a.Timestamp >= filter.DateFrom);
                if (filter.DateTo.HasValue)
                    iqu = iqu.Where(a => a.Timestamp <= filter.DateTo);
                if (!string.IsNullOrEmpty(filter.Type))
                    iqu = iqu.Where(a => a.EventType.Type == filter.Type);
            }
            var result = iqu.Select(a => new EventLogDto
            {
                Duration = a.Duration,
                EventType = a.EventType.Type,
                Timestamp = a.Timestamp,
                MsIsdnNumber = a.MsIsdn.MsIsdnNumber
            }).ToList();
            return result;
        }

        public List<EventTypeDto> GetEventTypes()
        {
            return _context.EventTypes.Select(a => new EventTypeDto { Type = a.Type, EventTypeId = a.EventTypeId }).ToList();
        }
    }
}
