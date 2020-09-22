using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChannelEngine
{
    public class Repository : IRepository
    {

        private static readonly string baseUrl = "https://api-dev.channelengine.net/api/v2/";
        private static readonly string orderSuffix = "orders/";
        private static readonly string productSuffix = "products/";
        private static readonly string inProgressQuery = "?statuses=IN_PROGRESS";
        private static readonly string apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        public Repository()
        {

        }

        public async Task<IList<Order>> GetOrdersInProgress()
        {
            IList<Order> result = null;
            using (var client = CreateHttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(baseUrl + orderSuffix + inProgressQuery)))
                {
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ChannelEngineResponse<Order>>(content).Content;
                }
            }
            return result;
        }

        public async Task<IList<Product>> GetProductsByMerchantProductNumber(string[] merchantProductNumbers)
        {
            IList<Product> result = null;
            string merchantProductNumberQuery = string.Empty;
            foreach (string s in merchantProductNumbers)
            {
                merchantProductNumberQuery = "&merchantProductNoList=" + s;
            }
            merchantProductNumberQuery = merchantProductNumberQuery.Substring(1);

            using (var client = CreateHttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(baseUrl + productSuffix + merchantProductNumberQuery)))
                {
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ChannelEngineResponse<Product>>(content).Content;
                }
            }

            return result;
        }

        public async Task<string> UpdateStockForProduct(string merchantProductNumber, int stock)
        {
            string result = null;

            using (var client = CreateHttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("Patch"), new Uri(baseUrl + productSuffix + merchantProductNumber)))
                {
                    // pretty sure this could have been a lot prettier in .NET Core. 
                    var patches = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            { "op", "replace" },
                            { "path", "Stock" },
                            { "value", stock.ToString() }
                        }
                    };

                    var content = JsonConvert.SerializeObject(patches);
                    request.Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json-patch");
                    var response = await client.SendAsync(request);
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient result = new HttpClient();

            result.DefaultRequestHeaders.Accept.Clear();
            result.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            result.DefaultRequestHeaders.Add("X-CE-KEY", apiKey);

            return result;
        }
    }
}
