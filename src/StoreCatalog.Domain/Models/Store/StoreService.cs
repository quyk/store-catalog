using StoreCatalog.Contract;
using StoreCatalog.Contract.Requests;
using StoreCatalog.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Store
{
    public class StoreService : IStoreService
    {
        #region "  Properties  "

        private readonly IProductService _productService;
        private readonly IAreaService _areaService;

        #endregion

        #region "  Constructor  "

        public StoreService(IProductService productService, IAreaService areaService)
        {
            _productService = productService;
            _areaService = areaService;
        }

        #endregion

        #region "  IStoreService  "

        public async Task<Ready> CheckStoreStatus()
        {
            var products = await _productService.GetProductsAsync(new ProductRequest() { StoreName = "Los Angeles - Pasadena" });
            var area = await _areaService.GetAreaAsync();

            if ((products != null && products.Count() > 0) && area != null)
            {
                return new Ready()
                {
                    IsReady = true,
                    StoreId = products.FirstOrDefault().StoreId
                };
            }

            return null;
        }

        #endregion
    }
}
