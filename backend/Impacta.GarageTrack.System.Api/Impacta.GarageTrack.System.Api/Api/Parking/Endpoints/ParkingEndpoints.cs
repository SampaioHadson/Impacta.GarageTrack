using Impacta.GarageTrack.System.Api.Api.Kernel;
using Impacta.GarageTrack.System.Api.Api.Parking.Dto;
using Impacta.GarageTrack.System.Api.Application.Parking.Commands;
using Impacta.GarageTrack.System.Api.Application.Parking.Queries;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GarageTrack.System.Api.Api.Parking.Endpoints
{
    public static class ParkingEndpoints
    {
        public static void MapParkingEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("Parking")
            .WithDescription("Endpoints de estacionamento")
            .WithTags("Parking");

            group.MapGet(string.Empty, async
                (
                    HttpContext context,
                    [FromServices] IListParkingQuery handler
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var queryRequest = new ListParkingQuery.Request(user.CompanyId);
                var result = await handler.Handle(queryRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<IEnumerable<ParkingItemVo>>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<IEnumerable<ParkingItemVo>>(result.ErrorMessage!));
            });

            group.MapGet("{id:long}", async
                (
                    HttpContext context,
                    [FromServices] IGetParkingByIdQuery handler,
                    [FromRoute] long id
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var queryRequest = new GetParkingByIdQuery.Request(id);
                var result = await handler.Handle(queryRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<ParkingItemVo>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<ParkingItemVo>(result.ErrorMessage!));
            });

            group.MapPost(string.Empty, async 
                (
                    HttpContext context,
                    [FromServices] IAddParkingCommand handler,
                    [FromBody] AddParkingRequestDto request
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var commandRequest = new AddParkingCommand.Request(request.Plate, request.Color, user.UserId, user.CompanyId);
                var result = await handler.HandleAsync(commandRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<ParkingItemVo>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<ParkingItemVo>(result.ErrorMessage!));
            });
        }
    }
}
