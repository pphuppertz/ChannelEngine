using System;
using System.Threading.Tasks;
using ChannelEngine;
using ChannelEngineBL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestChannelEngine
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public async Task TestGetOrdersInProgress()
        {
            //arrange
            IRepository repository = new MockedRepository();
            ChannelEngineController controller = new ChannelEngineController(repository);

            //act
            var orders = await controller.GetOutstandingOrders();

            Assert.IsTrue(orders.Count == 6);
        }

        [TestMethod]
        public async Task TestGetTopNProductsInOutstandingOrders()
        {
            //arrange
            IRepository repository = new MockedRepository();
            ChannelEngineController controller = new ChannelEngineController(repository);
            var orders = await controller.GetOutstandingOrders();

            //act
            var topNProducts = controller.TakeTopNProductsFromOrders(orders, 5);

            Assert.IsTrue(topNProducts.Count == 4);
            Assert.IsTrue(topNProducts[0].Name == "T-shirt met lange mouw BASIC petrol: M");
        }
    }
}
