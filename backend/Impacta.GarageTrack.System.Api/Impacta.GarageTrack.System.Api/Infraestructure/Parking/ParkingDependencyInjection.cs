using Impacta.GarageTrack.System.Api.Application.Parking.Commands;
using Impacta.GarageTrack.System.Api.Application.Parking.Queries;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Parking
{
    public static class ParkingDependencyInjection
    {
        public static void AddParkingDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAddParkingCommand, AddParkingCommand>();
            services.AddScoped<IGetParkingByIdQuery, GetParkingByIdQuery>();
            services.AddScoped<IListParkingQuery, ListParkingQuery>();
        }
    }
}
