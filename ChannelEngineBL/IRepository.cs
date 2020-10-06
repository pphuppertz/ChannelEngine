using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineBL
{
    public interface IRepository
    {
        Task<IList<Order>> GetOrdersInProgress();
        Task<Product> GetProductByMerchantProductNumber(string merchantProductNumber);
        Task<string> UpdateStockForProduct(string merchantProductNumber, int stock);
    }
}
