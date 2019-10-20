using System;

namespace StoreCatalog.Contract
{
    public interface IReady
    {
        Guid StoreId { get; set; }
        bool Ready { get; set; }
    }
}
