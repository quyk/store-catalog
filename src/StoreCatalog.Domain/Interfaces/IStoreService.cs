using StoreCatalog.Contract;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IStoreService
    {
        /// <summary>
        /// Check if Store has Areas and Products
        /// </summary>
        /// <returns><see cref="Ready"/></returns>
        Task<Ready> CheckStoreStatus();
    }
}
