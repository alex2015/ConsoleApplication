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
    }
}
