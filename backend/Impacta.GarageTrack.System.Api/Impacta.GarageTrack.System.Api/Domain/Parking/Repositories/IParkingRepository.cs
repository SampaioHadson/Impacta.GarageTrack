using Impacta.GarageTrack.System.Api.Domain.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;

namespace Impacta.GarageTrack.System.Api.Domain.Parking.Repositories
{
    public interface IParkingRepository : IRepositoryBase<Entities.Parking, long>
    {
        Task<ParkingItemVo?> ReaderGetByIdAsync(long id);
        Task<IEnumerable<ParkingItemVo>> ReaderGetAllByCompanyIdAsync(long companyId);
    }
}
