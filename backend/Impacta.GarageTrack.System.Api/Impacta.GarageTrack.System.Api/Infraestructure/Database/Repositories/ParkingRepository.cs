using Impacta.GarageTrack.System.Api.Domain.Parking.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;
using Microsoft.EntityFrameworkCore;
using ParkingEntity = Impacta.GarageTrack.System.Api.Domain.Parking.Entities.Parking;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories
{
    public class ParkingRepository : RepositoryBase<ParkingEntity, long>, IParkingRepository
    {
        public ParkingRepository(GarageTrackContext context) : base(context)
        {
        }

        public async Task<ParkingItemVo?> ReaderGetByIdAsync(long id)
        {
           return await _context.Parkings
                .Where(w => w.Id == id && w.IsActive)
                .Select(s => new ParkingItemVo
                {
                    Id = s.Id,
                    Plate = s.Plate,
                    Color = s.Color,
                    CreatedByUserId = s.CreatedByUser!.Id,
                    CreatedByUserName = s.CreatedByUser!.Name,
                    FinishedByUserId = s.FinishedByUser.Id,
                    FinishedByUserName = s.FinishedByUser.Name,
                    EntryTime = s.EntryTime,
                    ExitTime = s.ExitTime
                })
                .FirstOrDefaultAsync(); 
        }

        public async Task<IEnumerable<ParkingItemVo>> ReaderGetAllByCompanyIdAsync(long companyId)
        {
            return await _context.Parkings
                .Where(w => w.CompanyId == companyId && w.IsActive)
                .Select(s => new ParkingItemVo
                {
                    Id = s.Id,
                    Plate = s.Plate,
                    Color = s.Color,
                    CreatedByUserId = s.CreatedByUser!.Id,
                    CreatedByUserName = s.CreatedByUser!.Name,
                    FinishedByUserId = s.FinishedByUser.Id,
                    FinishedByUserName = s.FinishedByUser.Name,
                    EntryTime = s.EntryTime,
                    ExitTime = s.ExitTime
                })
                .ToListAsync();
        }
    }
}
