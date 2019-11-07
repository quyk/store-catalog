using StoreCatalog.Domain.Models.Area;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IAreaService
    {
        Task<AreasModel> GetAreaAsync();
    }
}
