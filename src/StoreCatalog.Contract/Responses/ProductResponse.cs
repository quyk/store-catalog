using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    public class ProductResponse
    {
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IList<ItemResponse> Items { get; set; }
        public string Price { get; set; }

        public ProductResponse() { }
    }
}
