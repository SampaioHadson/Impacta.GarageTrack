using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.ModelBuilders
{
    public class CompanyModelBuilder : IEntityTypeConfiguration<Domain.Company.Entities.Company>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Company.Entities.Company> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
