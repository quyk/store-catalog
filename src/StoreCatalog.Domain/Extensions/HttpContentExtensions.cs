using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Extensions
{
    /// <summary>
    /// Usefull HttpContext extensions to make life easier
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// Read a <see cref="HttpContent"/>, deserialize as JSON and cast to a T object
        /// </summary>
        /// <typeparam name="T">Final json cast</typeparam>
        /// <param name="content">HttpContent</param>
        /// <returns>Content deserialized as T</returns>
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        /// <summary>
        /// Serialize any <see cref="object"/> as JSON
        /// </summary>
        /// <param name="value">Any <see cref="object"/></param>
        /// <returns>A <see cref="HttpContent"/> with object serialized as json</returns>
        public static HttpContent SerializeAsJson(this object value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            return new StringContent(serialized, Encoding.Default, "application/json");
        }
    }
}
