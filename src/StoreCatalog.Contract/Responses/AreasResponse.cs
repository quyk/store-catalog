using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    /// <summary>
    /// Entity expected as response of api/production/areas
    /// </summary>
    public class AreasResponse
    {
        /// <summary>
        /// Production Unique Identifier
        /// </summary>
        public Guid ProductionId { get; set; }

        /// <summary>
        /// List of dietary restrictions
        /// </summary>
        public IList<string> Restrictions { get; set; }

        /// <summary>
        /// Indicates if store is on or off
        /// </summary>
        public bool On { get; set; }

        public AreasResponse() { }
    }
}
