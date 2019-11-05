using AutoMapper;
using GeekBurger.Products.Contract;
using StoreCatalog.Contract.Responses;

namespace StoreCatalog.Api.Profiles
{
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductToGet, ProductResponse>();
            CreateMap<ProductResponse, ProductToGet>();
        }
    }
}
