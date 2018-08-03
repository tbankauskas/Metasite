using Metasite.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Metasite.Repositories.Dtos;
using Metasite.Repositories.Interfaces;

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
            //arrange
            _dataRepository.Setup(a => a.GetEventTypes()).Returns(new List<EventTypeDto>());

            //act
            var result = _controller.GetEventTypes();

            //assert
            AssertType(result);
        }

        [TestMethod]
        public void GetWhenFilterNullTest()
        {
            //arrange
            _dataRepository.Setup(a => a.GetEventLog(null)).Returns(new List<EventLogDto>());

            //act
            var result = _controller.Get(null);

            //assert
            AssertType(result);
        }

        [TestMethod]
        public void GetWhenFilterNotNullTest()
        {
            //arrange
            _dataRepository.Setup(a => a.GetEventLog(It.IsAny<FilterDto>())).Returns(new List<EventLogDto>());

            //act
            var result = _controller.Get(It.IsAny<FilterDto>());

            //assert
            AssertType(result);
        }

        [TestMethod]
        public void GetTopsWhenFilterNullTest()
        {
            //arrange
            _dataRepository.Setup(a => a.GetTops(null)).Throws<NullReferenceException>();

            //act assert
            Assert.ThrowsException<NullReferenceException>(() => _controller.GetTops(null));
        }

        [TestMethod]
        public void GetTopsWhenFilterNotNullTest()
        {
            //arrange
            _dataRepository.Setup(a => a.GetTops(It.IsAny<FilterDto>())).Returns(new List<EventTopDto>());

            //act
            var result = _controller.GetTops(It.IsAny<FilterDto>());

            //assert
            AssertType(result);
        }

        private static void AssertType<T>(T result)
        {
            Assert.IsInstanceOfType(result, typeof(T));
        }
    }
}
