using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Requests
{
    public class ProductRequest
    {
        public string StoreName { get; set; }
        public Guid UserId { get; set; }
        public IList<string> Restrictions { get; set; }
    }
}
