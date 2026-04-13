using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.ModelBuilders
{
    public class ParkingTaxModeBuilder : IEntityTypeConfiguration<Domain.ParkingTax.Entities.ParkingTax>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.ParkingTax.Entities.ParkingTax> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .HasColumnType("numeric(18,2)");
        }
    }
}
