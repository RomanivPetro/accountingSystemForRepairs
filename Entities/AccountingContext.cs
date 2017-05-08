namespace Entities
{
    using System.Data.Entity;

    public partial class AccountingContext : DbContext
    {
        public AccountingContext()
            : base("name=AccountingContext")
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Spending> Spending { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .Property(e => e.Income)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Worker)
                .WithMany(e => e.Order)
                .Map(m => m.ToTable("WorkerOrders").MapLeftKey("OrderId").MapRightKey("WorkerId"));

            modelBuilder.Entity<Spending>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);
        }
    }
}
