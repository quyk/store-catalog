using AutoMapper;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract.Responses;

namespace StoreCatalog.Api.Profiles
{
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductModel, IProductResponse>();
            CreateMap<IProductResponse, ProductModel>();
        }
    }
}
