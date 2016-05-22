namespace GFT.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class GFTMarketDatabase : DbContext
    {
        public GFTMarketDatabase()
            : base("GFTMarketDatabase")
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
