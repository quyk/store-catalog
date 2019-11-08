using StoreCatalog.Domain.Models.Area;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    /// <summary>
    /// Area Service
    /// </summary>
    public interface IAreaService
    {
        /// <summary>
        /// Retrieve all available <see cref="AreasModel"/>
        /// </summary>
        /// <returns>A IEnumerable of AreasModel</returns>
        Task<IEnumerable<AreasModel>> GetAreaAsync();

        /// <summary>
        /// Update a <see cref="AreasModel"/> record
        /// </summary>
        /// <param name="area">The <see cref="AreasModel"/> updated</param>
        /// <returns></returns>
        Task Upsert(AreasModel area);
    }
}
