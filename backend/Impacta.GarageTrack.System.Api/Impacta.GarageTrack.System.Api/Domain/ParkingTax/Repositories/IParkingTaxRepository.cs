using Impacta.GarageTrack.System.Api.Domain.Kernel;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;

namespace Impacta.GarageTrack.System.Api.Domain.ParkingTax.Repositories
{
    public interface IParkingTaxRepository : IRepositoryBase<Entities.ParkingTax, long>
    {
        Task<ParkingTaxItemVo?> ReaderGetByIdAsync(long id);
        Task<IEnumerable<ParkingTaxItemVo>> ReaderGetAllByCompanyIdAsync(long companyId);
    }
}
