using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Requests
{
    /// <summary>
    /// Entity expected as parameter on api/products request
    /// </summary>
    public class ProductRequest
    {
        /// <summary>
        /// Store Name
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// User Unique Identifier
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// List of dietary restrictions of the user
        /// </summary>
        public IList<string> Restrictions { get; set; }
    }
}
