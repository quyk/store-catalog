using AutoMapper;
using GeekBurger.Products.Contract;
using StoreCatalog.Contract.Responses;

namespace StoreCatalog.Api.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemToGet, ItemResponse>();
            CreateMap<ItemResponse, ItemToGet>();
        }
    }
}
