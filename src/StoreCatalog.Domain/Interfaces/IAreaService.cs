using StoreCatalog.Domain.Models.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreasModel>> GetAreaAsync();
        Task Upsert(AreasModel area);
    }
}
