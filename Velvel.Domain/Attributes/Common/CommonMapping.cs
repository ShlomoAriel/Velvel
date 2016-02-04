using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Attributes.Common
{
    public class StatusMapping : EntityTypeConfiguration<Status>
    {
        public StatusMapping()
        {
            ToTable("Statuses");
            HasKey(x => x.Id);
        }
    }
    public class MeasurementUnitMapping : EntityTypeConfiguration<MeasurementUnit>
    {
        public MeasurementUnitMapping()
        {
            ToTable("MeasurementUnits");
            HasKey(x => x.Id);
        }
    }
}
