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

        private static string baseUrl = "https://api-dev.channelengine.net/api/v2/";
        private static string orderSuffix = "orders/";
        private static string productSuffix = "products/";
        private static string inProgressQuery = "?statuses=IN_PROGRESS";
        private static string apiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

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

        public async Task<IList<Product>> GetProductsByMerchantProductNumber(string[] MerchantProductNumbers)
        {
            IList<Product> result = null;
            string merchantNumberQuery = string.Empty;
            foreach(string s in MerchantProductNumbers)
            {
                merchantNumberQuery = "&merchantProductNoList=" + s;
            }
            merchantNumberQuery = merchantNumberQuery.Substring(1);

            using (var client = CreateHttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(baseUrl + productSuffix + merchantNumberQuery)))
                {
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ChannelEngineResponse<Product>>(content).Content;
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
