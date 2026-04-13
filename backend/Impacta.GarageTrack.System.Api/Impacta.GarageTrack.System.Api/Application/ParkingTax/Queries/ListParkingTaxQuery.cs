using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;

namespace Impacta.GarageTrack.System.Api.Application.ParkingTax.Queries
{
    public class ListParkingTaxQuery : IListParkingTaxQuery
    {
        private readonly IUnityOfWork _unityOfWork;

        public ListParkingTaxQuery(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<IEnumerable<ParkingTaxItemVo>>> Handle(Request request)
        {
            var result = await _unityOfWork.ParkingTaxRepository.ReaderGetAllByCompanyIdAsync(request.CompanyId);
            return Result<IEnumerable<ParkingTaxItemVo>>.Success(result);
        }

        public record Request(long CompanyId);
    }

    public interface IListParkingTaxQuery : IQuery<ListParkingTaxQuery.Request, Result<IEnumerable<ParkingTaxItemVo>>>
    {
    }
}
