using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;

namespace Impacta.GarageTrack.System.Api.Application.Parking.Queries
{
    public class ListParkingQuery : IListParkingQuery
    {
        private readonly IUnityOfWork _unityOfWork;

        public ListParkingQuery(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<IEnumerable<ParkingItemVo>>> Handle(Request request)
        {
            var result = await _unityOfWork.ParkingRepository.ReaderGetAllByCompanyIdAsync(request.CompanyId);
            return Result<IEnumerable<ParkingItemVo>>.Success(result);
        }

        public record Request(long CompanyId);
    }

    public interface IListParkingQuery : IQuery<ListParkingQuery.Request, Result<IEnumerable<ParkingItemVo>>>
    {
    }
}
