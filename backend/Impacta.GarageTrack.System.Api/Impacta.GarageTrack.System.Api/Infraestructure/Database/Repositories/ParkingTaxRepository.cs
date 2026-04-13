using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Repositories;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;
using Microsoft.EntityFrameworkCore;
using ParkingTaxEntity = Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities.ParkingTax;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories
{
    public class ParkingTaxRepository : RepositoryBase<ParkingTaxEntity, long>, IParkingTaxRepository
    {
        public ParkingTaxRepository(GarageTrackContext context) : base(context)
        {
        }

        public async Task<ParkingTaxItemVo?> ReaderGetByIdAsync(long id)
        {
            return await _context.ParkingTaxes
                .Where(w => w.Id == id && w.IsActive)
                .Select(s => new ParkingTaxItemVo
                {
                    Id = s.Id,
                    Type = s.Type,
                    Minutes = s.Minutes,
                    Value = s.Value,
                    CompanyId = s.CompanyId,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ParkingTaxItemVo>> ReaderGetAllByCompanyIdAsync(long companyId)
        {
            return await _context.ParkingTaxes
                .Where(w => w.CompanyId == companyId && w.IsActive)
                .Select(s => new ParkingTaxItemVo
                {
                    Id = s.Id,
                    Type = s.Type,
                    Minutes = s.Minutes,
                    Value = s.Value,
                    CompanyId = s.CompanyId,
                    CreatedAt = s.CreatedAt,
                    UpdatedAt = s.UpdatedAt
                })
                .ToListAsync();
        }
    }
}
