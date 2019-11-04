using AutoMapper;
using StoreCatalog.Api.Models;
using StoreCatalog.Contract.Responses;

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
