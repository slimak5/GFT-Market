namespace GFT.Services.TransactionProcessor.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feeds")]
    public partial class FeedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [Required]
        [StringLength(50)]
        public string OperationType { get; set; }
    }
}
