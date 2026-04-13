using Impacta.GarageTrack.System.Api.Application.ParkingTax.Commands;
using Impacta.GarageTrack.System.Api.Application.ParkingTax.Queries;

namespace Impacta.GarageTrack.System.Api.Infraestructure.ParkingTax
{
    public static class ParkingTaxDependencyInjection
    {
        public static void AddParkingTaxDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAddParkingTaxCommand, AddParkingTaxCommand>();
            services.AddScoped<IUpdateParkingTaxCommand, UpdateParkingTaxCommand>();
            services.AddScoped<IDeleteParkingTaxCommand, DeleteParkingTaxCommand>();
            services.AddScoped<IListParkingTaxQuery, ListParkingTaxQuery>();
        }
    }
}
