using System.Collections.Generic;
using Metasite.DAL.Dtos;

namespace Metasite.DAL.Interfaces
{
    public interface IDataRepository
    {
        List<EventsCountDto> GetEventsCountByType(int eventTypeId);
        List<EventTypeDto> GetEventTypes();
    }
}
