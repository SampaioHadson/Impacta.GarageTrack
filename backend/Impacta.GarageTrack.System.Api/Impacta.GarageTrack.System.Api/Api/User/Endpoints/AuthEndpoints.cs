using Impacta.GarageTrack.System.Api.Api.Kernel;
using Impacta.GarageTrack.System.Api.Application.User.Commands;

namespace Impacta.GarageTrack.System.Api.Api.User.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("Auth")
            .WithDescription("Endpoints de autenticação")
            .WithTags("Auth");

            group.MapPost("/login", async (IUserLoginCommand handler, UserLoginCommand.Request request) =>
            {
                var result = await handler.HandleAsync(request);
                return result.IsSuccess
                ? Results.Ok(new ResponseBase<UserLoginCommand.Response>(result.Value!)) 
                : Results.BadRequest(new ResponseBase<UserLoginCommand.Response>(result.ErrorMessage!));
            });
        }
    }
}
