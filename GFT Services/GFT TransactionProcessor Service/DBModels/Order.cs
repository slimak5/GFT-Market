namespace GFT.Services.TransactionProcessor.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderType { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public int ItemID { get; set; }

        public virtual Item Item { get; set; }
    }
}
