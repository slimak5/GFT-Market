namespace GFT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Database.Interfaces;
    using System.Xml.Serialization;
    using System.Web.Script.Serialization;

    [Table("Transactions")]
    public class Transaction : IDatabaseEntity
    {
        [Key]
        public int transactionId { get; set; }
        [Required]
        public int clientId { get; set; }
        [Required]
        [XmlIgnore]
        [ScriptIgnore]
        public DateTime transactionDate { get; set; }
        [Required]
        public int sellOrderId { get; set; }
        [Required]
        public int buyOrderId { get; set; }
        [Required]
        public virtual Item orderedItem { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int price { get; set; }


        public T GetInstance<T>()
            where T : class
        {
            return (T)Convert.ChangeType(this, typeof(T));
        }

        public static Transaction GenerateTransactionObject(Order sellOrder, Order buyOrder)
        {
            var transaction = new Transaction()
            {
                buyOrderId = buyOrder.orderId,
                clientId = buyOrder.clientId,
                orderedItem = buyOrder.item,
                price = sellOrder.price,
                quantity = CalcQuantity(sellOrder, buyOrder),
                sellOrderId = sellOrder.orderId,
                transactionDate = DateTime.Now,
                transactionId = new Random().Next(10000, 99999)
            };

            return transaction;
        }

        private static int CalcQuantity(Order sellOrder, Order buyOrder)
        {
            if(sellOrder.quantity > buyOrder.quantity)
            {
                sellOrder.quantity -= buyOrder.quantity;
                return buyOrder.quantity;
            }
            else if(sellOrder.quantity == buyOrder.quantity)
            {
                var quantity = sellOrder.quantity;
                sellOrder.quantity = 0;
                buyOrder.quantity = 0;
                return quantity;
            }
            else
            {
                buyOrder.quantity -= sellOrder.quantity;
                sellOrder.quantity = 0;
                return sellOrder.quantity;
            }
        }
    }
}
