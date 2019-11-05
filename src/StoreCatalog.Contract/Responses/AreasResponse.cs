using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    public class AreasResponse
    {
        public Guid ProductionId { get; set; }
        public IList<string> Restrictions { get; set; }
        public bool On { get; set; }

        public AreasResponse() { }
    }
}
