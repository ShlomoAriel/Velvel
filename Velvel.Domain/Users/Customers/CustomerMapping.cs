using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Users.Customers
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            ToTable("Customers");
            HasKey(x => x.Id);
        }
    }
}
