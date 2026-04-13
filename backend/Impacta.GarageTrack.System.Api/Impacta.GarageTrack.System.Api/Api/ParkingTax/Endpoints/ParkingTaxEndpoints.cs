using Impacta.GarageTrack.System.Api.Api.Kernel;
using Impacta.GarageTrack.System.Api.Api.ParkingTax.Dto;
using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Application.ParkingTax.Commands;
using Impacta.GarageTrack.System.Api.Application.ParkingTax.Queries;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;
using Microsoft.AspNetCore.Mvc;

namespace Impacta.GarageTrack.System.Api.Api.ParkingTax.Endpoints
{
    public static class ParkingTaxEndpoints
    {
        public static void MapParkingTaxEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("ParkingTax")
                .WithDescription("Endpoints de configuração de tarifas do estacionamento")
                .WithTags("ParkingTax");

            group.MapGet(string.Empty, async
                (
                    HttpContext context,
                    [FromServices] IListParkingTaxQuery handler
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var queryRequest = new ListParkingTaxQuery.Request(user.CompanyId);
                var result = await handler.Handle(queryRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<IEnumerable<ParkingTaxItemVo>>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<IEnumerable<ParkingTaxItemVo>>(result.ErrorMessage!));
            });

            group.MapPost(string.Empty, async
                (
                    HttpContext context,
                    [FromServices] IAddParkingTaxCommand handler,
                    [FromBody] AddParkingTaxRequestDto request
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var commandRequest = new AddParkingTaxCommand.Request(request.Type, request.Minutes, request.Value, user.CompanyId);
                var result = await handler.HandleAsync(commandRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<ParkingTaxItemVo>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<ParkingTaxItemVo>(result.ErrorMessage!));
            });

            group.MapPut("{id:long}", async
                (
                    HttpContext context,
                    [FromServices] IUpdateParkingTaxCommand handler,
                    [FromRoute] long id,
                    [FromBody] UpdateParkingTaxRequestDto request
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var commandRequest = new UpdateParkingTaxCommand.Request(id, request.Type, request.Minutes, request.Value, user.CompanyId);
                var result = await handler.HandleAsync(commandRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<ParkingTaxItemVo>(result.Value!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<ParkingTaxItemVo>(result.ErrorMessage!));
            });

            group.MapDelete("{id:long}", async
                (
                    HttpContext context,
                    [FromServices] IDeleteParkingTaxCommand handler,
                    [FromRoute] long id
                ) =>
            {
                var user = context.GetCurrentUser();
                if (user is null)
                    return TypedResults.Unauthorized();

                var commandRequest = new DeleteParkingTaxCommand.Request(id, user.CompanyId);
                var result = await handler.HandleAsync(commandRequest);
                return result.IsSuccess
                    ? TypedResults.Ok(new ResponseBase<object>(null!))
                    : (IResult)TypedResults.UnprocessableEntity(new ResponseBase<object>(result.ErrorMessage!));
            });
        }
    }
}
