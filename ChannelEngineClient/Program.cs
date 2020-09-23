using ChannelEngineBL;
using System;
using System.Threading.Tasks;

namespace ChannelEngineClient
{
    class Program
    {
        static async Task Main()
        {
            var controller = new ChannelEngineController();
            var orders = await controller.GetOutstandingOrders();

            Console.WriteLine("Found {0} orders in progress", orders.Count);
            Console.WriteLine();

            var top5Lines = controller.TakeTopNProductsFromOrders(orders, 5);

            Console.WriteLine("Products in outstanding orders:");
            foreach (var line in top5Lines)
            {
                Console.WriteLine("Product: {0} \t{1} \t {2} \t{3}", line.Name, line.Gtin, line.MerchantProductNumber, line.Quantity);                 
            }
            Console.ReadKey();

            Console.WriteLine("Setting quantity for top product to 25...");
            string result = await controller.UpdateStockForProduct(top5Lines[0].MerchantProductNumber, 25);
            Console.WriteLine(result);
            Console.WriteLine("... all done. Press any key to end this program.");
        }
    }
}
