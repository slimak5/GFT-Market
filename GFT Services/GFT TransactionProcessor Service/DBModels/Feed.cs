namespace GFT.Services.TransactionProcessor.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Feed
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(50)]
        public string OperationType { get; set; }
    }
}
