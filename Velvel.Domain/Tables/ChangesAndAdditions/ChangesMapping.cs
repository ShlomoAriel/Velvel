using System.Data.Entity.ModelConfiguration;
using Velvel.Domain.Tables.ChangesAndAdditions;

namespace Velvel.Domain.ChangesAndAdditions
{
    public class ChangesMapping : EntityTypeConfiguration<Changes>
    {
        public ChangesMapping()
        {
            ToTable("Changes");
            HasKey(x => x.Id);
        }
    }
}
