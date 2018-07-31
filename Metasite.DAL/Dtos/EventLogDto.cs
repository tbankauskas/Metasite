using System;

namespace Metasite.DAL.Dtos
{
    public class EventLogDto: EventBaseDto
    {
        public string EventType { get; set; }
        public int? Duration { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
