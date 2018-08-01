using Metasite.DAL;
using Metasite.DAL.Dtos;
using Metasite.DAL.Interfaces;
using Metasite.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metasite.UnitTests.Tests.Repositories
{
    [TestClass]
    public class DataRepositoryTests
    {
        private IDataRepository _dataRepository;
        private DbContextOptions<MSContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<MSContext>()
                .UseInMemoryDatabase(databaseName: "MSContextDatabase")
                .Options;
            PrepareDatabaseData();
        }

        private void PrepareDatabaseData()
        {
            using (var context = new MSContext(_options))
            {
                if (!context.EventTypes.Any())
                {
                    EntitiesBuilder.BuildEntities();
                    context.EventTypes.AddRange(EntitiesBuilder.EventTypes);
                    context.MsIsdns.AddRange(EntitiesBuilder.MsIsdns);
                    context.EventLogs.AddRange(EntitiesBuilder.EventLogs);
                    context.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void GetEventLogWhenFilterNull()
        {
            using (var context = new MSContext(_options))
            {
                _dataRepository = new DataRepository(context);
                var result = _dataRepository.GetEventLog(null);

                Assert.IsInstanceOfType(result, typeof(List<EventLogDto>));
                Assert.AreEqual(6, result.Count);
            }
        }

        [TestMethod]
        public void GetEventLogWhenFilterNotNull()
        {
            using (var context = new MSContext(_options))
            {
                _dataRepository = new DataRepository(context);

                //when DateFrom.HasValue
                var result = _dataRepository.GetEventLog(new FilterDto { DateFrom = DateTime.Now.AddDays(1) });
                Assert.IsInstanceOfType(result, typeof(List<EventLogDto>));
                Assert.AreEqual(5, result.Count);

                //when DateTo.HasValue
                result = _dataRepository.GetEventLog(new FilterDto { DateTo = DateTime.Now.AddDays(3) });
                Assert.IsInstanceOfType(result, typeof(List<EventLogDto>));
                Assert.AreEqual(3, result.Count);

                //when Type not null
                result = _dataRepository.GetEventLog(new FilterDto { Type = "sms" });
                Assert.IsInstanceOfType(result, typeof(List<EventLogDto>));
                Assert.IsFalse(result.Any(a => a.EventType == "call"));

                //when passed filter should return count zero
                result = _dataRepository.GetEventLog(new FilterDto { DateFrom = DateTime.Now, DateTo = DateTime.Now, Type = "MMS" });
                Assert.IsInstanceOfType(result, typeof(List<EventLogDto>));
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public void GetTopsWhenFilterNull()
        {
            using (var context = new MSContext(_options))
            {
                _dataRepository = new DataRepository(context);
                Assert.ThrowsException<NullReferenceException>(() => _dataRepository.GetTops(null));
            }
        }

        [TestMethod]
        public void GetTopsWhenFilterNotNull()
        {
            using (var context = new MSContext(_options))
            {
                _dataRepository = new DataRepository(context);

                //when Type = "sms"
                var result = _dataRepository.GetTops(new FilterDto { Type = "sms" });
                Assert.IsInstanceOfType(result, typeof(List<EventTopDto>));
                Assert.IsTrue(result.Count <= 5);

                //when Type = "call"
                result = _dataRepository.GetTops(new FilterDto { Type = "call" });
                Assert.IsInstanceOfType(result, typeof(List<EventTopDto>));
                Assert.IsTrue(result.Count <= 5);

                //when none existing type passed
                result = _dataRepository.GetTops(new FilterDto { Type = "mms" });
                Assert.IsInstanceOfType(result, typeof(List<EventTopDto>));
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public void GetEventTypesTest()
        {
            using (var context = new MSContext(_options))
            {
                _dataRepository = new DataRepository(context);
                var result = _dataRepository.GetEventTypes();

                Assert.IsInstanceOfType(result, typeof(List<EventTypeDto>));
                Assert.AreEqual(2, result.Count);
            }
        }
    }
}