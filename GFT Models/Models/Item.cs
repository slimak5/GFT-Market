namespace GFT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Items")]
    public class Item
    {
        [Key]
        public int itemId { get; set; }
        [Required]
        public string itemName { get; set; }
        [Required]
        public string supportedServiceId { get; set; }
    }
}
