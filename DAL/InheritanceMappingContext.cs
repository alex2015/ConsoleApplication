using System.Data.Entity;

namespace DAL
{
    public class InheritanceMappingContext : DbContext
    {
        public InheritanceMappingContext() : base("DbConnection")
        {
        }

        public DbSet<BillingDetail_TPH> BillingDetail_TPHs { get; set; }
    }
}
