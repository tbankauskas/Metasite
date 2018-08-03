using System;

namespace Metasite.Repositories.Dtos
{
    public class EventLogDto: EventBaseDto
    {
        public string EventType { get; set; }
        public int? Duration { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
