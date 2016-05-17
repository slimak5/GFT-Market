namespace GFT.Services.TransactionProcessor.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orders")]
    public partial class OrderEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int ItemID { get; set; }
        public virtual ItemEntity Item { get; set; }
    }
}
