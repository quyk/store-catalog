using StoreCatalog.Contract;
using StoreCatalog.Contract.Requests;
using StoreCatalog.Domain.Enums;
using StoreCatalog.Domain.Extensions;
using StoreCatalog.Domain.Interfaces;
using StoreCatalog.Domain.ServiceBus.Topic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Models.Store
{
    public class StoreService : IStoreService
    {
        #region "  Properties  "

        private readonly IProductService _productService;
        private readonly IAreaService _areaService;
        private readonly ITopicBus _topicBus;

        #endregion

        #region "  Constructor  "

        public StoreService(IProductService productService, IAreaService areaService,
            ITopicBus topicBus)
        {
            _productService = productService;
            _areaService = areaService;
            _topicBus = topicBus;
        }

        #endregion

        #region "  IStoreService  "

        public async Task<Ready> CheckStoreStatus()
        {
            var products = await _productService.GetProductsAsync(new ProductRequest() { StoreName = "Los Angeles - Pasadena" });
            var area = await _areaService.GetAreaAsync();

            if ((products != null && products.Count() > 0) && area != null)
            {
                var storeId = products.FirstOrDefault().StoreId;

                await _topicBus.SendAsync(TopicType.StoreCatalogReady.GetDescription(), $"Store: {storeId}. Status: {true}");

                return new Ready()
                {
                    IsReady = true,
                    StoreId = storeId
                };
            }

            return null;
        }

        #endregion
    }
}
