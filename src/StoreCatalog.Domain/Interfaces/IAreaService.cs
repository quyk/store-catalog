using StoreCatalog.Contract.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreasResponse>> GetAreaAsync();
    }
}
