namespace GFT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
   
    [Table("Items")]
    public class Item : Database.Interfaces.IDatabaseEntity
    {
        [Key]
        public int itemId { get; set; }
        [Required]
        public string itemName { get; set; }
        [Required]
        public string supportedServiceId { get; set; }

        public T GetInstance<T>()
            where T : class
        {
            return (T)Convert.ChangeType(this, typeof(T));
        }
    }
}
