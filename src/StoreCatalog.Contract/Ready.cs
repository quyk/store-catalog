using System;

namespace StoreCatalog.Contract
{
    public class Ready
    {
        public Guid StoreId { get; set; }
        public bool IsReady { get; set; }
    }
}
