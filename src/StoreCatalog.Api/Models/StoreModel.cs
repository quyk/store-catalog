using System;

namespace StoreCatalog.Api.Models
{
    public class StoreModel
    {
        public Guid StoreId { get; set; }
        public bool Ready { get; set; }
    }
}
