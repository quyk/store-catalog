using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StoreCatalog.Domain.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static HttpContent SerializeAsJson(this object value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            return new StringContent(serialized, Encoding.Default, "application/json");
        }
    }
}
