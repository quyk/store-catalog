using AutoMapper;
using Bogus;
using FluentAssertions;
using GeekBurger.Products.Contract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreCatalog.Api.Controllers;
using StoreCatalog.Contract.Requests;
using StoreCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StoreCatalog.Api.Test.Controllers
{
    public class ProductControllerTest
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productService;
        private readonly Mock<IMapper> _mapper;
        private readonly Faker _faker;
        private readonly ProductRequest _productRequest;
        private readonly IEnumerable<ProductToGet> _productToGet;

        public ProductControllerTest()
        {
            _faker = new Faker();
            _productService = new Mock<IProductService>();
            _mapper = new Mock<IMapper>();
            _productController = new ProductController(_productService.Object, _mapper.Object);
            _productRequest = new ProductRequest
            {
                UserId = _faker.Random.Guid(),
                StoreName = _faker.Name.FirstName(),
                Restrictions = Enumerable.Range(2, 5)
                    .Select(_ => _faker.Name.FirstName())
                    .ToList()
            };

            _productToGet = Enumerable.Range(2, 5)
                .Select(_ => new ProductToGet
                {
                    ProductId = _faker.Random.Guid(),
                    StoreId = _faker.Random.Guid(),
                    Image = _faker.Image.PicsumUrl(),
                    Name = _faker.Name.FullName(),
                    Price = _faker.Random.Decimal(1),
                });
        }


        [Fact]
        public async Task TestGetProductAsyncBeSucess()
        {
            _productService.Setup(x => x.GetProductsAsync(_productRequest)).ReturnsAsync(_productToGet);
            var callback = await _productController.GetProductAsync(_productRequest);

            callback.Should().BeAssignableTo<OkObjectResult>();
        }

        [Fact]
        public async Task TestGetProductAsyncBeNotFound()
        {
            _productService.Setup(x => x.GetProductsAsync(_productRequest)).ReturnsAsync((IEnumerable<ProductToGet>) null);
            var callback = await _productController.GetProductAsync(_productRequest);

            callback.Should().BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task TestGetProductAsyncBeBadRequest()
        {
            _productService.Setup(x => x.GetProductsAsync(_productRequest)).Throws<Exception>();
            var callback = await _productController.GetProductAsync(_productRequest);

            callback.Should().BeAssignableTo<BadRequestObjectResult>();
        }
    }
}
