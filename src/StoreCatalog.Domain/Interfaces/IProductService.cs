using GeekBurger.Products.Contract;
using StoreCatalog.Contract.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Interfaces
{
    /// <summary>
    /// Product Service Abstraction
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieve all available <see cref="ProductToGet"/>
        /// </summary>
        /// <param name="productRequest">Object that encapsulate StoreName, UserId and Restrictions</param>
        /// <returns>A IEnumerable of <see cref="ProductToGet"/></returns>
        Task<IEnumerable<ProductToGet>> GetProductsAsync(ProductRequest productRequest);

        /// <summary>
        /// Update a <see cref="ProductToGet"/> record
        /// </summary>
        /// <param name="product">The <see cref="ProductToGet"/> updated</param>
        /// <returns></returns>
        Task Upsert(ProductToGet product);
    }
}
