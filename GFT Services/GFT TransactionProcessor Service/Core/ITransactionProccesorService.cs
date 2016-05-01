﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Messaging;
using GFT.Services.TransactionProcessor.DBModels;

namespace GFT.Services.TransactionProcessor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITransactionProcessor
    {
        [OperationContract(IsOneWay = true)]
        void processOrders();

        [OperationContract(IsOneWay = true)]
        void matchOrders();

        [OperationContract(IsOneWay = true)]
        void sendSupportedItems();
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
        
    }
}

