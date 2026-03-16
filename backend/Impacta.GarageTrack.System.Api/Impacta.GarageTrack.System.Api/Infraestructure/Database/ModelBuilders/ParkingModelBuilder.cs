using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.ModelBuilders
{
    public class ParkingModelBuilder : IEntityTypeConfiguration<Domain.Parking.Entities.Parking>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Parking.Entities.Parking> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
