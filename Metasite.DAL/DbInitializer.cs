using Metasite.DAL.Entities;
using System;
using System.Linq;

namespace Metasite.DAL
{
    public class DbInitializer
    {
        private static readonly DateTime startDate = new DateTime(2015, 1, 1);

        public static void Initialize(MSContext context)
        {
            context.Database.EnsureCreated();
            if (context.MsIsdns.Any())
            {
                return;
            }

            var random = new Random();

            context.EventTypes.AddRange(
                new EventType() { EventTypeId = 1, Type = "sms" },
                new EventType() { EventTypeId = 2, Type = "call" });

            for (int i = 1; i <= 100; i++)
            {
                string finalNumber = GetRandomNumber(random);
                if (context.MsIsdns.FirstOrDefault(a => a.MsIsdnNumber == finalNumber) != null)
                    continue;
                var ms = new MsIsdn()
                {
                    MsIsdnId = i,
                    MsIsdnNumber = finalNumber
                };
                context.MsIsdns.Add(ms);
            }
            
            foreach (var item in context.MsIsdns.Local.ToList())
            {
                var rnd = random.Next(15);
                for (int i = 0; i < rnd; i++)
                {
                    var log = new EventLog()
                    {
                        Duration = random.Next(300),
                        EventTypeId = random.Next(1, 3),
                        Timestamp = GetRandomDatetime(random),
                        MsIsdn = item
                    };
                    context.EventLogs.Add(log);
                }
            }

            context.SaveChanges();
        }

        private static string GetRandomNumber(Random random)
        {
            var rndInt = random.Next(1, 99999);
            var intLen = (int)Math.Log10(rndInt) + 1;
            var finalNumber = $"370623{ string.Concat(Enumerable.Repeat("0", 5 - intLen))}{rndInt}";
            return finalNumber;
        }

        private static DateTime GetRandomDatetime(Random random)
        {
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(random.Next(range));
        }
    }
}

