using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GFT.Services.TransactionProcessor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITransactionProcessor
    {
        //TODO add operations
        [OperationContract(IsOneWay = true)]
        public void sendItem(Item item);
        [OperationContract(IsOneWay = true)]
        public void sendFeed(Feed item);
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

