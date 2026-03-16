using Impacta.GarageTrack.System.Api.Api.Parking.Endpoints;
using Impacta.GarageTrack.System.Api.Api.User.Endpoints;
using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Company.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Parking.Repositories;
using Impacta.GarageTrack.System.Api.Infraestructure.Database;
using Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories;
using Impacta.GarageTrack.System.Api.Infraestructure.Parking;
using Impacta.GarageTrack.System.Api.Infraestructure.User;
using Impacta.GarageTrack.System.Api.Infraestructure.User.Services;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GarageTrackContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();

            services.AddUserDependencies();
            services.AddParkingDependencies();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }


        public static void AddEndpoints(this WebApplication app)
        {
            app.MapAuthEndpoints();
            app.MapParkingEndpoints();
        }
    }
}
