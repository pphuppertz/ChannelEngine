using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine
{
    public interface IRepository
    {
        Task<IList<Order>> GetOrdersInProgress();
        Task<IList<Product>> GetProductsByMerchantProductNumber(string[] MerchantProductNumbers);
    }
}
