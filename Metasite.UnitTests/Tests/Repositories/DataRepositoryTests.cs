using Metasite.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Metasite.Repositories.Dtos;
using Metasite.Repositories.Helpers;
using Metasite.Repositories.Interfaces;
using Metasite.Repositories.Repositories;

namespace Metasite.UnitTests.Tests.Repositories
{
    [TestClass]
    public class DataRepositoryTests
    {
        private IDataRepository _dataRepository;
        private DbContextOptions<MContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<MContext>()
                .UseInMemoryDatabase("MContextDatabase")
                .Options;
            PrepareDatabaseData();
        }

        private void PrepareDatabaseData()
        {
            using (var context = new MContext(_options))
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
            using (var context = new MContext(_options))
            {
                //arrange
                _dataRepository = new DataRepository(context);

                //act
                var result = _dataRepository.GetEventLog(null);

                //assert
                AssertType(result);
                Assert.AreEqual(6, result.Count);
            }
        }

        [TestMethod]
        public void GetEventLogWhenFilterNotNull()
        {
            using (var context = new MContext(_options))
            {
                //arrange
                _dataRepository = new DataRepository(context);

                //act when DateFrom.HasValue
                var result = _dataRepository.GetEventLog(new FilterDto { DateFrom = DateTime.Now.AddDays(1) });
                AssertType(result);
                Assert.AreEqual(5, result.Count);

                //act when DateTo.HasValue
                result = _dataRepository.GetEventLog(new FilterDto { DateTo = DateTime.Now.AddDays(3) });
                AssertType(result);
                Assert.AreEqual(3, result.Count);

                //act when Type not null
                result = _dataRepository.GetEventLog(new FilterDto { Type = EventTypeEnum.Sms });
                AssertType(result);
                Assert.IsFalse(result.Any(a => a.EventType == EventTypeEnum.Call));

                //act when passed filter should return count zero
                result = _dataRepository.GetEventLog(new FilterDto { DateFrom = DateTime.Now, DateTo = DateTime.Now, Type = "MMS" });
                AssertType(result);
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public void GetTopsWhenFilterNull()
        {
            using (var context = new MContext(_options))
            {
                //arrange
                _dataRepository = new DataRepository(context);
                //act assert
                Assert.ThrowsException<NullReferenceException>(() => _dataRepository.GetTops(null));
            }
        }

        [TestMethod]
        public void GetTopsWhenFilterNotNull()
        {
            using (var context = new MContext(_options))
            {
                //arrange
                _dataRepository = new DataRepository(context);

                //act when Type = "sms"
                var result = _dataRepository.GetTops(new FilterDto { Type = EventTypeEnum.Sms });
                AssertType(result);
                Assert.IsTrue(result.Count <= 5);

                //act when Type = "call"
                result = _dataRepository.GetTops(new FilterDto { Type = EventTypeEnum.Call });
                AssertType(result);
                Assert.IsTrue(result.Count <= 5);

                //act when none existing type passed
                result = _dataRepository.GetTops(new FilterDto { Type = "mms" });
                AssertType(result);
                Assert.AreEqual(0, result.Count);
            }
        }

        [TestMethod]
        public void GetEventTypesTest()
        {
            using (var context = new MContext(_options))
            {
                //arrange
                _dataRepository = new DataRepository(context);

                //act
                var result = _dataRepository.GetEventTypes();

                //assert
                AssertType(result);
                Assert.AreEqual(2, result.Count);
            }
        }

        private static void AssertType<T>(T result)
        {
            Assert.IsInstanceOfType(result, typeof(T));
        }
    }
}