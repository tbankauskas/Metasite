﻿using Metasite.DAL.Entities;
using System;
using System.Linq;

namespace Metasite.DAL
{
    public class DbInitializer
    {
        private static readonly DateTime StartDate = new DateTime(2015, 1, 1);

        public static void Initialize(MContext context)
        {
            context.Database.EnsureCreated();
            if (context.MsIsdns.Any())
            {
                return;
            }

            var random = new Random();

            //adding event types
            context.EventTypes.AddRange(
                new EventType { EventTypeId = 1, Type = "sms" },
                new EventType { EventTypeId = 2, Type = "call" });

            //adding 100 recods to MsIsdn witn random numbers
            for (var i = 1; i <= 100; i++)
            {
                var finalNumber = GetRandomNumber(random);
                if (context.MsIsdns.FirstOrDefault(a => a.MsIsdnNumber == finalNumber) != null)
                    continue; //if already exist do not add cause number is unique
                var ms = new MsIsdn
                {
                    MsIsdnId = i,
                    MsIsdnNumber = finalNumber
                };
                context.MsIsdns.Add(ms);
            }

            //for each MsIsdn number generating random number event log records
            foreach (var item in context.MsIsdns.Local.ToList())
            {
                var rnd = random.Next(15);
                for (var i = 0; i < rnd; i++)
                {
                    var log = new EventLog { EventTypeId = random.Next(1, 3) };
                    log.Duration = log.EventTypeId == 1 ? (int?)null : random.Next(300);
                    log.Timestamp = GetRandomDatetime(random);
                    log.MsIsdn = item;
                    context.EventLogs.Add(log);
                }
            }

            context.SaveChanges();
        }

        //generate random msisdn number with some patern
        private static string GetRandomNumber(Random random)
        {
            var rndInt = random.Next(1, 99999);
            var intLen = (int)Math.Log10(rndInt) + 1;
            var finalNumber = $"370623{ string.Concat(Enumerable.Repeat("0", 5 - intLen))}{rndInt}";
            return finalNumber;
        }

        //generate random datetime value from StartDate till today
        private static DateTime GetRandomDatetime(Random random)
        {
            var range = (DateTime.Today - StartDate).Days;
            return StartDate.AddDays(random.Next(range))
                .AddHours(random.Next(0, 24))
                .AddMinutes(random.Next(0, 60))
                .AddSeconds(random.Next(0, 60))
                .AddMilliseconds(random.Next(0, 1000));
        }
    }
}

