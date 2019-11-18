using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using StoreCatalog.Api.Controllers;
using StoreCatalog.Contract;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using StoreCatalog.Domain.Suports.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace StoreCatalog.Api.Test.Controllers
{
    public class StoreControllerTest
    {
        private readonly StoreController _storeController;
        private readonly Mock<IStoreService> _storeService;
        private readonly Mock<ITopicBus> _topicBus;
        private readonly IOptions<ServiceBusOption> _option;
        private readonly Ready _ready;
        private readonly Faker _faker;

        public StoreControllerTest()
        {
            _faker = new Faker();
            _storeService = new Mock<IStoreService>();
            _topicBus = new Mock<ITopicBus>();
            _option = Options.Create(new ServiceBusOption
            {
                ResourceGroup = _faker.Random.AlphaNumeric(10),
                NamespaceName = _faker.Random.AlphaNumeric(10),
                ConnectionString = _faker.Random.AlphaNumeric(10),
                ClientId = _faker.Random.AlphaNumeric(10),
                ClientSecret = _faker.Random.AlphaNumeric(10),
                SubscriptionId = _faker.Random.AlphaNumeric(10),
                TenantId = _faker.Random.AlphaNumeric(10),
                ServiceBus = new ServiceBus
                {
                    Store = _faker.Name.FindName(),
                    TopicLog = _faker.Random.AlphaNumeric(10),
                    Product = new ServiceBusFilter
                    {
                        Topic = _faker.Name.FirstName(),
                        Subscription = _faker.Name.FirstName()
                    },
                    ProductionArea = new ServiceBusFilter
                    {
                        Topic = _faker.Name.FirstName(),
                        Subscription = _faker.Name.FirstName()
                    }
                }
            });
            _storeController = new StoreController(_storeService.Object, _topicBus.Object, _option);
            _ready = new Ready
            {
                IsReady = _faker.Random.Bool(),
                StoreId = _faker.Random.Guid()
            };
        }

        [Fact]
        public async Task TestGetStoreAsyncBeSucess()
        {
            _topicBus.Setup(x => x.SendAsync(_option.Value.ServiceBus.TopicLog, "Calling Get Store.."));
            _storeService.Setup(x => x.CheckStoreStatus()).ReturnsAsync(_ready);

            var callback = await _storeController.GetStoreAsync();

            callback.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async Task TestGetStoreAsyncBeNotFound()
        {
            _topicBus.Setup(x => x.SendAsync(_option.Value.ServiceBus.TopicLog, "Calling Get Store.."));
            _storeService.Setup(x => x.CheckStoreStatus()).ReturnsAsync((Ready) null);

            var callback = await _storeController.GetStoreAsync();

            callback.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task TestGetStoreAsyncBeBadRequest()
        {
            _topicBus.Setup(x => x.SendAsync(_option.Value.ServiceBus.TopicLog, "Calling Get Store.."));
            _storeService.Setup(x => x.CheckStoreStatus()).Throws<Exception>();

            var callback = await _storeController.GetStoreAsync();

            callback.Should().BeAssignableTo<BadRequestObjectResult>();
        }
    }
}
