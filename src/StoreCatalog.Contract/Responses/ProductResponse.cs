using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    /// <summary>
    /// Entity expected as response of api/products request
    /// </summary>
    public class ProductResponse
    {
        /// <summary>
        /// Store Unique Identifier
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// Product Unique Identifier
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product Image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// List of <see cref="ItemResponse"/>
        /// </summary>
        public IList<ItemResponse> Items { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public string Price { get; set; }

        public ProductResponse() { }
    }
}
