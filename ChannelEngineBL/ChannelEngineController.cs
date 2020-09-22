using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelEngine;

namespace ChannelEngineBL
{
    public class ChannelEngineController
    {

        private readonly IRepository repository = null;

        public ChannelEngineController() 
        {
            repository = new Repository();
        }

        public ChannelEngineController(IRepository repository)
        {
            if (repository != null)
            {
                this.repository = repository;
            }
            else
            {
                this.repository = new Repository();
            }
        }

        public async Task<IList<Order>> GetOutstandingOrders() 
        {
            var orders = await repository.GetOrdersInProgress();
            return orders;
        }

        public IList<OrderLine>TakeTopNProductsFromOrders(IList<Order> orders, int take)
        {
            IList<OrderLine> result = null;
            List<OrderLine> foundLines = new List<OrderLine>();
            foreach (var order in orders)
            {
                foreach (var line in order.Lines)
                {
                    OrderLine foundLine = foundLines.FirstOrDefault(l => l.MerchantProductNo == line.MerchantProductNo);
                    if (foundLine != null)
                    {
                        foundLine.Quantity += line.Quantity;
                    }
                    else
                    {
                        foundLines.Add(line);
                    }
                }
            }
            result = foundLines.OrderByDescending(l => l.Quantity).ThenBy(l => l.Description).Take(take).ToList();

            return result;            
        }

        public async Task<string> UpdateStockForProduct(string merchantProductNumber, int stockQuantity)
        {
            string result = await repository.UpdateStockForProduct(merchantProductNumber, stockQuantity);
            return result;
        }
    }
}
