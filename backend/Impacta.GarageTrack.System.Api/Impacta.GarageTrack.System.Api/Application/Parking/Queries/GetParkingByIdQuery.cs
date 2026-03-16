using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;

namespace Impacta.GarageTrack.System.Api.Application.Parking.Queries
{
    public class GetParkingByIdQuery : IGetParkingByIdQuery
    {
        private readonly IUnityOfWork _unityOfWork;

        public GetParkingByIdQuery(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<ParkingItemVo>> Handle(Request request)
        {
            var result = await _unityOfWork.ParkingRepository.ReaderGetByIdAsync(request.Id);
            if (result is null)
                return Result<ParkingItemVo>.Failure("Registro não encontrado.");

            return Result<ParkingItemVo>.Success(result);
        }

        public record Request(long Id);
    }

    public interface IGetParkingByIdQuery : IQuery<GetParkingByIdQuery.Request, Result<ParkingItemVo>>
    {
    }
}
