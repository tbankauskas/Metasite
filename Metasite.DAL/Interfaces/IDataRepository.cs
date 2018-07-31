using System.Collections.Generic;
using Metasite.DAL.Dtos;

namespace Metasite.DAL.Interfaces
{
    public interface IDataRepository
    {
        List<EventLogDto> GetEventLog(FilterDto filter);
        List<EventTypeDto> GetEventTypes();
    }
}
