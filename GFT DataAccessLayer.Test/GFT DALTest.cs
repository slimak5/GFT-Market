using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GFT.Database;

namespace GFT.Database.Test
{
    [TestClass]
    public class DataAcessLayerTests
    {
        GFTMarketDatabaseAccessObject Database;

        [TestMethod]
        public void DALCanPerformReadOperations()
        {
            Database = new GFTMarketDatabaseAccessObject(new GFTMarketDatabase());

            Models.Item expectedItem = new Models.Item
            {
                itemId = 1,
                itemName = "T-Bone",
                supportedServiceId = "BAK1"
            };

            Assert.AreEqual(
                expectedItem.itemId,
                Database.ReadSingle<Models.Item>(1).itemId);
            Assert.AreEqual(
                expectedItem.itemName,
                Database.ReadSingle<Models.Item>(1).itemName);
            Assert.AreEqual(
                expectedItem.supportedServiceId,
                Database.ReadSingle<Models.Item>(1).supportedServiceId);
        }

        [TestMethod]
        public void DALCanPerformUpdateOperations()
        {
            Database = new GFTMarketDatabaseAccessObject(new GFTMarketDatabase());

            Models.Item updatedItem = new Models.Item
            {
                itemId = 18,
                itemName = "T-Test",
                supportedServiceId = "BAK1"
            };

            Database.Update(updatedItem);

            Assert.AreEqual(
                updatedItem.itemId,
                Database.ReadSingle<Models.Item>(18).itemId);
            Assert.AreEqual(
                updatedItem.itemName,
                Database.ReadSingle<Models.Item>(18).itemName);
            Assert.AreEqual(
                updatedItem.supportedServiceId,
                Database.ReadSingle<Models.Item>(18).supportedServiceId);
        }
    }
}
