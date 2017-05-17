using System.Data.Entity;

namespace DAL
{
    public class InheritanceMappingContext : DbContext
    {
        public InheritanceMappingContext() : base("DbConnection")
        {
        }

        public DbSet<BillingDetail_TPH> BillingDetail_TPHs { get; set; }

        public DbSet<BillingDetail_TPT> BillingDetail_TPTs { get; set; }

        public DbSet<User_TPT> User_TPTs { get; set; }

        public DbSet<BillingDetail_TPC> BillingDetail_TPCs { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount_TPC>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("BankAccount_TPC");
            });

            modelBuilder.Entity<CreditCard_TPC>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("CreditCard_TPC");
            });
        }
    }
}
