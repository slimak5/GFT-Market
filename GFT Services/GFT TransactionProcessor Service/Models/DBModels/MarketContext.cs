namespace GFT.Services.TransactionProcessor.DbModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MarketDatabase : DbContext
    {
        public MarketDatabase()
            : base("name=MarketContext")
        {
        }

        public virtual DbSet<FeedEntity> Feeds { get; set; }
        public virtual DbSet<ItemEntity> Items { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedEntity>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<FeedEntity>()
                .Property(e => e.OperationType)
                .IsUnicode(false);

            modelBuilder.Entity<ItemEntity>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ItemEntity>()
                .Property(e => e.SupportedBackend)
                .IsUnicode(false);

            modelBuilder.Entity<ItemEntity>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderEntity>()
                .Property(e => e.OrderType)
                .IsUnicode(false);

        }
    }
}
