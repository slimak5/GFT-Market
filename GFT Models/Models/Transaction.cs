namespace GFT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int transactionId { get; set; }
        [Required]
        public string clientId { get; set; }
        [Required]
        public DateTime transactionDate { get; set; }
        [Required]
        public string sellOrderId { get; set; }
        [Required]
        public string buyOrderId { get; set; }
        [Required]
        public virtual Item orderedItem { get; set; }
    }
}
