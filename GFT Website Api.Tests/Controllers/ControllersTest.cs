using Microsoft.VisualStudio.TestTools.UnitTesting;
using GFT.Website.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace GFT.Website.Api.Controllers.Tests
{
    [TestClass()]
    public class ItemsControllerTests
    {

        [TestMethod()]
        public void BuyItemTest()
        {
            HttpClient httpClient = new HttpClient() { BaseAddress = new System.Uri("http://localhost:54919/api/"), };
            Task<HttpResponseMessage> message = httpClient.PostAsync("Items/BuyItem",
                new StringContent(new JavaScriptSerializer().Serialize(new Models.Item())));
            if (message.IsCompleted)
                Assert.AreEqual(HttpStatusCode.OK, message.Result.StatusCode);
        }

        [TestMethod()]
        public void SellItemTest()
        {
            HttpClient httpClient = new HttpClient() { BaseAddress = new System.Uri("http://localhost:54919/api/"), };
            Task<HttpResponseMessage> message = httpClient.PostAsync("Items/SellItem",
                new StringContent(new JavaScriptSerializer().Serialize(new Models.Item())));
            if (message.IsCompleted)
                Assert.AreEqual(HttpStatusCode.OK, message.Result.StatusCode);
        }

        [TestMethod()]
        public void GetItemsTest()
        {
            HttpClient httpClient = new HttpClient() { BaseAddress = new System.Uri("http://localhost:54919/api/"), };
            Task<HttpResponseMessage> message = httpClient.GetAsync("Items/GetItems");
            if (message.IsCompleted)
                Assert.AreEqual(HttpStatusCode.OK, message.Result.StatusCode);
        }

    }

    [TestClass()]
    public class FeedsControllerTests
    {

        [TestMethod()]
        public void GetFeedsTest()
        {
            HttpClient httpClient = new HttpClient() { BaseAddress = new System.Uri("http://localhost:54919/api/"), };
            Task<HttpResponseMessage> message = httpClient.GetAsync("Feeds/GetFeeds");
            if (message.IsCompleted)
            {
                Assert.AreEqual(HttpStatusCode.OK, message.Result.StatusCode);
                Assert.AreEqual(typeof(List<Models.Feed>),message.Result.Content.GetType());
            }
        }
    }
}