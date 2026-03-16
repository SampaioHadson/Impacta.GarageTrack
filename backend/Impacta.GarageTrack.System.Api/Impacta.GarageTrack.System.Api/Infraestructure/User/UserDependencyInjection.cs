using Impacta.GarageTrack.System.Api.Application.User.Commands;
using Impacta.GarageTrack.System.Api.Application.User.Services;
using Impacta.GarageTrack.System.Api.Domain.Users.Repositories;
using Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories;
using Impacta.GarageTrack.System.Api.Infraestructure.User.Services;

namespace Impacta.GarageTrack.System.Api.Infraestructure.User
{
    public static class UserDependencyInjection
    {
        public static void AddUserDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAddUserCommand, AddUserCommand>();
            services.AddScoped<IUserLoginCommand, UserLoginCommand>();
            services.AddScoped<IAuthenticatorService, AuthenticatorService>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
