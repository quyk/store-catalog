using AutoMapper;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Models.Area;

namespace StoreCatalog.Api.Profiles
{
    public class AreasModelProfile : Profile
    {
        public AreasModelProfile()
        {
            CreateMap<AreasModel, AreasResponse>();
            CreateMap<AreasResponse, AreasModel>();
        }
    }
}
