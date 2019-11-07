using StoreCatalog.Contract;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    public interface IStoreService
    {
        /// <summary>
        /// Check if Store has Areas and Products
        /// </summary>
        /// <returns>Ready entity</returns>
        Task<Ready> CheckStoreStatus();
    }
}
