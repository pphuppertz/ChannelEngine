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

        public IList<LineItem>TakeTopNProductsFromOrders(IList<Order> orders, int take)
        {
            var result = orders
                .SelectMany(o => o.Lines)
                .GroupBy(o => o.MerchantProductNo)
                .Select(l => new LineItem
                {
                    Gtin = l.First().Gtin,
                    MerchantProductNumber = l.First().MerchantProductNo,
                    Name = l.First().Description,
                    Quantity = l.Sum(q => q.Quantity)
                })
                .OrderByDescending(l => l.Quantity).ThenBy(l => l.Name).Take(take).ToList();
            // The MerchantProductNo is included because it is the only unique identifier for the product, 
            // at least in the data set provided. I am assuming that this is used by one merchant. If it is not unique, 
            // but a composite key with the EAN is, the MerchantProductNo is still needed for setting a quantity, 
            // as all T-shirts have the same EAN (Gtin), regardless of size.

            return result;          
        }

        public async Task<string> UpdateStockForProduct(string merchantProductNumber, int stockQuantity)
        {
            string result = await repository.UpdateStockForProduct(merchantProductNumber, stockQuantity);
            return result;
        }
    }
}
