using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.ModelBuilders
{
    public class UserModelBuilder : IEntityTypeConfiguration<Domain.Users.Entities.User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Users.Entities.User> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
