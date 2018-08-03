using Metasite.DAL.Entities;
using System;
using System.Collections.Generic;
using Metasite.Repositories.Helpers;

namespace Metasite.UnitTests
{
    public static class EntitiesBuilder
    {
        public static List<EventType> EventTypes { get; set; }
        public static List<EventLog> EventLogs { get; set; }
        public static List<MsIsdn> MsIsdns { get; set; }

        private static void BuildEventTypes()
        {
            EventTypes = new List<EventType>(){
                new EventType { EventTypeId = 1, Type = EventTypeEnum.Sms },
                new EventType { EventTypeId = 2, Type = EventTypeEnum.Call } }
            ;
        }

        private static void BuildMsIsdns()
        {
            MsIsdns = new List<MsIsdn>()
            {
                new MsIsdn{MsIsdnId = 1, MsIsdnNumber = "64565466"},
                new MsIsdn{MsIsdnId = 2, MsIsdnNumber = "48784adfs"}
            };
        }
        private static void BuildEventLog()
        {
            EventLogs = new List<EventLog>()
            {
                new EventLog
                {
                    EventLogId = 1, EventTypeId = 1, MsIsdnId = 1,
                    Timestamp = DateTime.Now.AddDays(1), EventType = EventTypes[0], MsIsdn = MsIsdns[0]
                },
                new EventLog
                {
                    EventLogId = 2,EventTypeId = 1, MsIsdnId = 1,
                    Timestamp = DateTime.Now.AddDays(2), EventType = EventTypes[0], MsIsdn = MsIsdns[0]
                },
                new EventLog
                {
                    EventLogId = 3, EventTypeId = 1, MsIsdnId = 2,
                    Timestamp = DateTime.Now.AddDays(3), EventType = EventTypes[0], MsIsdn = MsIsdns[1]
                },
                new EventLog
                {
                    EventLogId = 4, EventTypeId = 2, MsIsdnId = 2,
                    Timestamp = DateTime.Now.AddDays(4), EventType = EventTypes[1], MsIsdn = MsIsdns[1]
                },
                new EventLog
                {
                    EventLogId = 5, EventTypeId = 2, MsIsdnId = 2,
                    Timestamp = DateTime.Now.AddDays(5), EventType = EventTypes[1], MsIsdn = MsIsdns[1]
                },
                new EventLog
                {
                    EventLogId = 6, EventTypeId = 2, MsIsdnId = 1,
                    Timestamp = DateTime.Now.AddDays(6), EventType = EventTypes[1], MsIsdn = MsIsdns[1]
                }
            };
        }

        public static void BuildEntities()
        {
            BuildEventTypes();
            BuildMsIsdns();
            BuildEventLog();
        }
    }
}
