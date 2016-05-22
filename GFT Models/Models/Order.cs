using System.Runtime.Serialization;

namespace GFT.Models
{
    [DataContract]
    public class Order
    {
        public Order(Item item)
        {
            this.item = item;
            this.orderType = "n/a";
        }
        [DataMember]
        public int orderId { get; set; }
        [DataMember]
        public int clientId { get; set; }
        [DataMember]
        public Item item { get; set; }
        [DataMember]
        public int price { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public string orderType { get; set; }
    }
}