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

        IRepository repository = null;

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
            List<OrderLine> lines = new List<OrderLine>();
            foreach (var order in orders)
            {
                lines.AddRange(order.Lines);    
            }
            result = lines.OrderByDescending(l => l.Quantity).Take(take).ToList();

            return result;            
        }
    }
}
