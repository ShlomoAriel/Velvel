using System.Data.Entity.ModelConfiguration;
using Velvel.Domain.Attributes.Common;

namespace Velvel.Domain.Tables.Defects
{
    public class DefectMapping : EntityTypeConfiguration<Defect>
    {
        public DefectMapping()
        {
            ToTable("Defects");
            HasKey(x => x.Id);
        }
    }
}
