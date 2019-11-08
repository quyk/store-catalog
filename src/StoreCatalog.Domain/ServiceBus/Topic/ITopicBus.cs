using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.ServiceBus.Topic
{
    /// <summary>
    /// Interface of ServiceBus topic
    /// </summary>
    public interface ITopicBus
    {
        /// <summary>
        /// Send a message on a topic
        /// </summary>
        /// <param name="topic">Topic name</param>
        /// <param name="message">Message to send</param>
        /// <returns></returns>
        Task SendAsync(string topic, string message);

        /// <summary>
        /// Send a list of messages on a topic
        /// </summary>
        /// <param name="topic">Topic name</param>
        /// <param name="messages">List of messages to send</param>
        /// <returns></returns>
        Task SendAsync(string topic, IList<string> messages);
    }
}
