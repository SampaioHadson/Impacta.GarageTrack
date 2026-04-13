using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database
{
    public class GarageTrackContext : DbContext
    {
        public GarageTrackContext(DbContextOptions<GarageTrackContext> options) : base(options)
        {
        }

        public DbSet<Domain.Users.Entities.User> Users { get; set; }
        public DbSet<Domain.Company.Entities.Company> Companies { get; set; }
        public DbSet<Domain.Parking.Entities.Parking> Parkings { get; set; }
        public DbSet<Domain.ParkingTax.Entities.ParkingTax> ParkingTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GarageTrackContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            new Converters(configurationBuilder);
        }
    }
}
