using StoreCatalog.Contract.Responses;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IAreaService
    {
        Task<AreasResponse> GetAreaAsync();
    }
}
