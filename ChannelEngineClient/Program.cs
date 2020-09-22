using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelEngine;
using ChannelEngineBL;

namespace ChannelEngineClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var controller = new ChannelEngineController();
            var orders = await controller.GetOutstandingOrders();
            Console.WriteLine(orders.Count);

            var bl = new ChannelEngineController();
            var top5Lines = bl.TakeTopNProductsFromOrders(orders, 5);

            foreach(var line in top5Lines)
            {
                Console.WriteLine("Product: {0} {1} {2} {3}", line.Description, line.Gtin, line.MerchantProductNo, line.Quantity);
            }
            Console.ReadKey();
        }

        
    }
}
