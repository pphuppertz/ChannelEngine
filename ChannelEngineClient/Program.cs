using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelEngine;

namespace ChannelEngineClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var repository = new Repository();
            var orders = await repository.GetOrdersInProgress();
            Console.WriteLine(orders.Count);
        }

        
    }
}
