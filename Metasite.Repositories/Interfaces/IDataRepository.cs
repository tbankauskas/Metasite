using System.Collections.Generic;
using Metasite.Repositories.Dtos;

namespace Metasite.Repositories.Interfaces
{
    public interface IDataRepository
    {
        List<EventLogDto> GetEventLog(FilterDto filter);
        List<EventTopDto> GetTops(FilterDto filter);
        List<EventTypeDto> GetEventTypes();
    }
}
