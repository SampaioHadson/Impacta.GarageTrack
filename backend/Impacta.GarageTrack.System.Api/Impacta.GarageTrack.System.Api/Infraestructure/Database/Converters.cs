using Microsoft.EntityFrameworkCore;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database
{
    public class Converters
    {
        public Converters(ModelConfigurationBuilder configurationBuilder)
        {
            // Conversores globais - aplicados antes da criação do modelo
            configurationBuilder
                .Properties<DateTime>()
                .HaveColumnType("timestamp without time zone");
        }
    }
}
