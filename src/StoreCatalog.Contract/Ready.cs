using System;

namespace StoreCatalog.Contract
{
    /// <summary>
    /// Entity expected as response of api/store request
    /// </summary>
    public class Ready
    {
        /// <summary>
        /// Store Unique Identifier
        /// </summary>
        public Guid StoreId { get; set; }

        /// <summary>
        /// Indicates if store is ready or not
        /// </summary>
        public bool IsReady { get; set; }
    }
}
