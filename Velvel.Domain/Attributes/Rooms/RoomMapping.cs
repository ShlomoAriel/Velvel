using System.Data.Entity.ModelConfiguration;

namespace Velvel.Domain.Attributes.Rooms
{
    public class RoomMapping : EntityTypeConfiguration<Room>
    {
        public RoomMapping()
        {
            ToTable("Rooms");
            HasKey(x => x.Id);
            Property(x => x.Name).HasMaxLength(150);
        }
    }
}
