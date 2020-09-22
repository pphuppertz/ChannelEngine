using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelEngine;
using Newtonsoft.Json; 

namespace TestChannelEngine
{
    public class MockedRepository : ChannelEngine.IRepository
    {
        public async Task<IList<Order>> GetOrdersInProgress()
        {
            IList<Order> result = null;
            using (StreamReader jsonFile = File.OpenText("orders.json"))
            {
                string content = jsonFile.ReadToEnd();
                result = JsonConvert.DeserializeObject<ChannelEngineResponse<Order>>(content).Content;
            }
            await Task.Delay(100);
            return result;
        }

        public async Task<IList<Product>> GetProductsByMerchantProductNumber(string[] MerchantProductNumbers)
        {
            await Task.Delay(100);
            throw new NotImplementedException();
        }

        public async Task<string> UpdateStockForProduct(string merchantProductNumber, int stock)
        {
            await Task.Delay(100);
            throw new NotImplementedException();
        }
    }
}
