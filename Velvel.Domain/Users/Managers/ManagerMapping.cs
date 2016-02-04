using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Users.Managers
{
    public class CustomerMapping : EntityTypeConfiguration<Manager>
    {
        public CustomerMapping()
        {
            ToTable("Managers");
            HasKey(x => x.Id);
        }
    }
}
