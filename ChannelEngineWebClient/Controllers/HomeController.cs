using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ChannelEngineBL;
using ChannelEngineWebClient.Models;

namespace ChannelEngineWebClient.Controllers
{
    public class HomeController : Controller
    {
        readonly ChannelEngineHelper helper;

        public HomeController()
        {
            helper = new ChannelEngineHelper();
        }
        public async Task<ActionResult> Index()
        {            
            var orders = await helper.GetOutstandingOrders();
            var Top5Lines = helper.TakeTopNProductsFromOrders(orders, 5);

            return View(Top5Lines);
        }

        public async Task<ActionResult> Details(string merchantProductNumber)
        {
            var product = await helper.GetProductbyMerchantProductNumber(merchantProductNumber);
            var productDto = new ProductDto
            {
                Description = product.Description,
                Gti = product.Ean,
                MerchantProductNumber = product.MerchantProductNo,
                QuantityInStock = product.Stock
            };

            return View(productDto);
        }

        [HttpPost]
        public async Task<ActionResult> Details(int updateStock, string productNo)
        {
            await helper.UpdateStockForProduct(productNo, updateStock);
            return Redirect("Details?merchantProductNumber=" + productNo);
        }
    }
}