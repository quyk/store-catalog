using System.ComponentModel;

namespace StoreCatalog.Domain.Enums
{
    /// <summary>
    /// Represents available ServiceBus topics
    /// </summary>
    public enum TopicType
    {
        [Description("Product")]
        Product,

        [Description("ProductionArea")]
        ProductionArea,

        [Description("StoreCatalogReady")]
        StoreCatalogReady,

        [Description("UserWithLessOffer")]
        UserWithLessOffer,

        [Description("Log")]
        Log
    }
}
