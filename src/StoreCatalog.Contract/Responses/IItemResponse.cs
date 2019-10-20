using System;

namespace StoreCatalog.Contract.Responses
{
    public interface IItemResponse
    {
        Guid ItemId { get; set; }
        string Name { get; set; }
    }
}
