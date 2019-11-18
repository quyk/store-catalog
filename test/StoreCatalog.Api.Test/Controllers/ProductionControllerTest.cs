using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreCatalog.Api.Controllers;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.Models.Area;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StoreCatalog.Api.Test.Controllers
{
    public class ProductionControllerTest
    {
        private readonly ProductionController _productionController;
        private readonly Mock<IAreaService> _areaService;
        private readonly Mock<IMapper> _mapper;
        private readonly Faker _faker;
        private readonly AreasModel _areasModel;

        public ProductionControllerTest()
        {
            _faker = new Faker();
            _areaService = new Mock<IAreaService>();
            _mapper = new Mock<IMapper>();
            _productionController = new ProductionController(_areaService.Object, _mapper.Object);
            _areasModel = new AreasModel
            {
                On = _faker.Random.Bool(),
                ProductionId = _faker.Random.Guid(),
                Restrictions = Enumerable.Range(2, 5)
                .Select(_ => _faker.Name.FirstName())
                .ToList()
            };
        }

        [Fact]
        public async Task TestGetAreasAsyncBeSucess()
        {
            _areaService.Setup(x => x.GetAreaAsync()).ReturnsAsync(_areasModel);

            var callback = await _productionController.GetAreasAsync();

            callback.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async Task TestGetAreasAsyncBeNotFound()
        {
            _areaService.Setup(x => x.GetAreaAsync()).ReturnsAsync((AreasModel) null);

            var callback = await _productionController.GetAreasAsync();

            callback.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task TestGetAreasAsyncBeBadRequest()
        {
            _areaService.Setup(x => x.GetAreaAsync()).Throws<Exception>();

            var callback = await _productionController.GetAreasAsync();

            callback.Should().BeAssignableTo<BadRequestObjectResult>();
        }
    }
}
