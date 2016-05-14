using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;
using GFT.Services.TransactionProcessor.DBModels;

namespace GFT.Services.TransactionProcessor
{
    [ServiceContract]
    public interface ITransactionProcessor
    {
        [OperationContract(IsOneWay = true)]
        void start();

        [OperationContract(IsOneWay = true)]
        void stop();
    }

    [DataContract]
    public class Item
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public int price { get; set; }

        public static explicit operator Item(DBModels.Item v)
        {
            Item i = new Item();
            i.id = v.Id;
            i.name = v.Name;
            i.price = 0;
            i.quantity = 0;
            return i;
        }
    }

    [DataContract]
    public class Feed
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public int price { get; set; }

        public static explicit operator Feed(DBModels.Feed v)
        {
            Feed f = new Feed();
            f.id = v.Id;
            f.name = v.ItemName;
            f.quantity = v.Quantity;
            f.type = v.OperationType;
            f.price = v.Price;
            return f;
        }
    }
}

