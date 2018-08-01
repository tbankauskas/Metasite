using Metasite.Controllers;
using Metasite.DAL.Dtos;
using Metasite.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Metasite.UnitTests.Tests.Controllers
{
    [TestClass]
    public class DataControllerTests
    {
        private DataController _controller;
        private Mock<IDataRepository> _dataRepository;

        [TestInitialize]
        public void Initialize()
        {
            _dataRepository = new Mock<IDataRepository>();
            _controller = new DataController(_dataRepository.Object);
        }

        [TestMethod]
        public void GetEventTypesTest()
        {
            _dataRepository.Setup(a => a.GetEventTypes()).Returns(new List<EventTypeDto>());
            var result = _controller.GetEventTypes();

            Assert.IsInstanceOfType(result, typeof(IEnumerable<EventTypeDto>));
        }

        [TestMethod]
        public void GetWhenFilterNullTest()
        {
            _dataRepository.Setup(a => a.GetEventLog(null)).Returns(new List<EventLogDto>());
            var result = _controller.Get(null);

            Assert.IsInstanceOfType(result, typeof(IEnumerable<EventLogDto>));
        }

        [TestMethod]
        public void GetWhenFilterNotNullTest()
        {
            var filter = new FilterDto();
            _dataRepository.Setup(a => a.GetEventLog(It.IsAny<FilterDto>())).Returns(new List<EventLogDto>());
            var result = _controller.Get(It.IsAny<FilterDto>());

            Assert.IsInstanceOfType(result, typeof(IEnumerable<EventLogDto>));
        }

        [TestMethod]
        public void GetTopsWhenFilterNullTest()
        {
            _dataRepository.Setup(a => a.GetTops(null)).Throws<NullReferenceException>();
            Assert.ThrowsException<NullReferenceException>(() => _controller.GetTops(null));
        }

        [TestMethod]
        public void GetTopsWhenFilterNotNullTest()
        {
            _dataRepository.Setup(a => a.GetTops(It.IsAny<FilterDto>())).Returns(new List<EventTopDto>());
            var result = _controller.GetTops(It.IsAny<FilterDto>());

            Assert.IsInstanceOfType(result, typeof(IEnumerable<EventTopDto>));
        }
    }
}
