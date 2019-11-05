using System;
using System.Collections.Generic;

namespace StoreCatalog.Domain.Models.Area
{
    public class AreasModel
    {
        public Guid ProductionId { get; set; }
        public IList<string> Restrictions { get; set; }
        public bool On { get; set; }
    }
}
