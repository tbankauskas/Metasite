using System.Collections.Generic;
using System.Linq;
using Metasite.DAL;
using Metasite.DAL.Entities;
using Metasite.Repositories.Dtos;
using Metasite.Repositories.Helpers;
using Metasite.Repositories.Interfaces;

namespace Metasite.Repositories.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly MContext _context;

        public DataRepository(MContext context)
        {
            _context = context;
        }

        public List<EventLogDto> GetEventLog(FilterDto filter)
        {
            var iqu = ImplementFilters(filter);
            var result = iqu.Select(a => new EventLogDto
            {
                Duration = a.Duration,
                EventType = a.EventType.Type,
                Timestamp = a.Timestamp,
                MsIsdnNumber = a.MsIsdn.MsIsdnNumber
            }).ToList();
            return result;
        }

        public List<EventTopDto> GetTops(FilterDto filter)
        {
            var iqu = ImplementFilters(filter);
            IQueryable<EventTopDto> tops;
            if (filter.Type == EventTypeEnum.Sms)
                tops = iqu.GroupBy(a => a.MsIsdn.MsIsdnNumber)
                   .Select(g =>
                   new EventTopDto
                   {
                       MsIsdnNumber = g.Key,
                       Number = g.Count()
                   });
            else
                tops = iqu.GroupBy(a => a.MsIsdn.MsIsdnNumber)
                   .Select(g =>
                   new EventTopDto
                   {
                       MsIsdnNumber = g.Key,
                       Number = g.Sum(a => a.Duration == null ? 0 : (int)a.Duration)
                   });
            return tops.OrderByDescending(o => o.Number).Take(5).ToList();
        }

        private IQueryable<EventLog> ImplementFilters(FilterDto filter)
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

            return iqu;
        }

        public List<EventTypeDto> GetEventTypes()
        {
            return _context.EventTypes.Select(a => new EventTypeDto { Type = a.Type, EventTypeId = a.EventTypeId }).ToList();
        }
    }
}
