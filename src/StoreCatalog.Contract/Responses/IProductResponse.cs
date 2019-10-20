using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    public interface IProductResponse
    {
        Guid StoreId { get; set; }
        Guid ProductId { get; set; }
        string Name { get; set; }
        string Image { get; set; }
        IList<IItemResponse> Items { get; set; }
        string Price { get; set; }
    }
}
