using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Tables.Floorings
{
    public class FlooringMapping : EntityTypeConfiguration<Flooring>
    {
        public FlooringMapping()
        {
            ToTable("Flooring");
            HasKey(x => x.Id);
        }
    }
    public class ModelMapping : EntityTypeConfiguration<Model>
    {
        public ModelMapping()
        {
            ToTable("Model");
            HasKey(x => x.Id);
        }
    }
    public class GroutMapping : EntityTypeConfiguration<Grout>
    {
        public GroutMapping()
        {
            ToTable("Grout");
            HasKey(x => x.Id);
        }
    }
    //public class MeasurementsMapping : EntityTypeConfiguration<Measurement>
    //{
    //    public MeasurementsMapping()
    //    {
    //        ToTable("Measurement");
    //        HasKey(x => x.Id);
    //    }
    //}
}
