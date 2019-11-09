using AutoMapper;
using StoreCatalog.Contract.Responses;
using StoreCatalog.Domain.Models.Area;

namespace StoreCatalog.Api.Profiles
{
    /// <summary>
    /// AutoMapper profile to map <see cref="AreasModel"/> to <see cref="AreasResponse"/>
    /// </summary>
    public class AreasModelProfile : Profile
    {
        public AreasModelProfile()
        {
            CreateMap<AreasModel, AreasResponse>();
            CreateMap<AreasResponse, AreasModel>();
        }
    }
}
