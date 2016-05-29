using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Threading;

namespace GFT.Website.Api.Controllers.Tests
{
    [TestClass]
    public class ControllersTests
    {
        OrdersController _OrdersController;
        WebClientController _WebClientController;

        [TestMethod]
        public void WebclientCanGenerateClientId()
        {
            _WebClientController = new WebClientController();
            var result = _WebClientController.GenerateWebClientId();
            Assert.IsTrue( result > 9999 && result < 1000000);
        }

        [TestMethod]
        public void OrderControllerCanPassNewOrderToQueue()
        {
            _OrdersController = new OrdersController();

            var result = _OrdersController.SendBuyOrder(new Models.Order(new Models.Item()));
            Thread.Sleep(100);
            Assert.IsInstanceOfType(result, typeof(string));
        }
    }
}
