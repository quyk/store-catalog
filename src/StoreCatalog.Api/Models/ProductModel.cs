using System;
using System.Collections.Generic;

namespace StoreCatalog.Api.Models
{
    public class ProductModel
    {
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IList<ItemModel> Items { get; set; }
        public string Price { get; set; }
    }
}
