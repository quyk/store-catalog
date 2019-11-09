using AutoMapper;
using GeekBurger.Products.Contract;
using StoreCatalog.Contract.Responses;

namespace StoreCatalog.Api.Profiles
{
    /// <summary>
    /// AutoMapper profile to map <see cref="ProductToGet"/> to <see cref="ProductResponse"/>
    /// </summary>
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductToGet, ProductResponse>();
            CreateMap<ProductResponse, ProductToGet>();
        }
    }
}
