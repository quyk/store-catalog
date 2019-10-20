using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Requests
{
    public interface IProductRequest
    {
        string StoreName { get; set; }
        Guid UserId { get; set; }
        IList<string> Restrictions { get; set; }
    }
}
