using System;

namespace StoreCatalog.Contract.Responses
{
    public class ItemResponse
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
    }
}
