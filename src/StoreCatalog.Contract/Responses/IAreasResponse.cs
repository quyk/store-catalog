using System;
using System.Collections.Generic;

namespace StoreCatalog.Contract.Responses
{
    public interface IAreasResponse
    {
        Guid ProductionId { get; set; }
        IList<string> Restrictions { get; set; }
        bool On { get; set; }
    }
}
