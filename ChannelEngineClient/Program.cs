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
                Console.WriteLine("Product: {0} \t{1} \t {2} \t{3}", line.Description, line.Gtin, line.MerchantProductNo, line.Quantity);
                // the MerchantProductNo is included because it is the only unique identifier for the product, 
                // at least in the data set provided. I am assuming that this is used by one merchant. If it is not unique, 
                // but a composite key with the EAN is, the MerchantProductNo is still needed for setting a quantity, 
                // as all T-shirts have the same EAN (Gtin), regardless of size. 
            }
            Console.ReadKey();

            string result = await controller.UpdateStockForProduct(top5Lines[0].MerchantProductNo, 25);
            Console.WriteLine(result);
        }


    }
}
