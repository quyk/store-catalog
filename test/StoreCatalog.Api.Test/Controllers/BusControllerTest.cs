using Bogus;
using FluentAssertions;
using Moq;
using StoreCatalog.Api.Controllers;
using StoreCatalog.Api.Models;
using StoreCatalog.Domain.ServiceBus;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace StoreCatalog.Api.Test.Controllers
{
    public class BusControllerTest
    {
        private readonly BusController _busController;
        private readonly Mock<IQueueBus> _queueBus;
        private readonly Faker _faker;
        private readonly BusModel _busModel;

        public BusControllerTest()
        {
            _faker = new Faker();
            _queueBus = new Mock<IQueueBus>();
            _busController = new BusController(_queueBus.Object);
            _busModel = new BusModel
            {
                Message = _faker.Lorem.Paragraph()
            };
        }

        [Fact]
        public async Task TestSendAsyncBeSucess()
        {
            _queueBus.Setup(x => x.SendAsync(_busModel.Message));

            var callback = await _busController.SendAsync(_busModel);

            callback.Should().BeAssignableTo<CreatedResult>();
        }

        [Fact]
        public async Task TestSendAsyncBeBadRequest()
        {
            _queueBus.Setup(x => x.SendAsync(_busModel.Message)).Throws<Exception>();

            var callback = await _busController.SendAsync(_busModel);

            callback.Should().BeAssignableTo<BadRequestObjectResult>();
        }
    }
}
