namespace GFT.Services.TransactionProcessor.DBModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MarketContext : DbContext
    {
        public MarketContext()
            : base("name=MarketContext")
        {
        }

        public virtual DbSet<Feed> Feeds { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feed>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Feed>()
                .Property(e => e.OperationType)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.SupportedBackend)
                .IsUnicode(false);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderID)
                .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderType)
                .IsUnicode(false);
        }
    }
}
