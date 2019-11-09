using AutoMapper;
using GeekBurger.Products.Contract;
using StoreCatalog.Contract.Responses;

namespace StoreCatalog.Domain.Profiles
{
    /// <summary>
    /// AutoMapper profile to map <see cref="ItemToGet"/> to <see cref="ItemResponse"/>
    /// </summary>
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemToGet, ItemResponse>();
            CreateMap<ItemResponse, ItemToGet>();
        }
    }
}
