using System;

namespace StoreCatalog.Contract.Responses
{
    /// <summary>
    /// Entity expected as property of <see cref="ProductResponse"/>, the list of ingredients
    /// </summary>
    public class ItemResponse
    {
        /// <summary>
        /// Item Unique Identifier
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string Name { get; set; }

        public ItemResponse() { }
    }
}
