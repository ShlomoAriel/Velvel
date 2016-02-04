using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Tables.Plumbings
{
    public class PlumbingMapping : EntityTypeConfiguration<Plumbing>
    {
        public PlumbingMapping()
        {
            ToTable("Plumbing");
            HasKey(x => x.Id);
        }
    }
    public class AccessoryMapping : EntityTypeConfiguration<Plumbing>
    {
        public AccessoryMapping()
        {
            ToTable("Accessories");
            HasKey(x => x.Id);
        }
    }
}
