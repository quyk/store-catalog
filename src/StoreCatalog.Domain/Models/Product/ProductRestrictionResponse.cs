using System;
using System.Collections.Generic;

namespace StoreCatalog.Domain.Models.Product
{
    public class ProductRestrictionResponse
    {
        public Guid ProductId { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public ProductRestrictionResponse() { }
    }
}
