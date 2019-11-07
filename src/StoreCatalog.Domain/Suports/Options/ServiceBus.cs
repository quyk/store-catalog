namespace StoreCatalog.Domain.Suports.Options
{
    public class ServiceBus
    {
        public string Store { get; set; }
        public string TopicLog { get; set; }
        public ServiceBusFilter Product { get; set; }
        public ServiceBusFilter ProductionArea { get; set; }
    }
}
