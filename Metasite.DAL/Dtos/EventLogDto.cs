using System;

namespace Metasite.DAL.Dtos
{
    public class EventLogDto
    {
        public string MsIsdnNumber { get; set; }
        public string EventType { get; set; }
        public int? Duration { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
