using Impacta.GarageTrack.System.Api.Domain.Company.Repositories;
using Microsoft.EntityFrameworkCore;
using CompanyEntity = Impacta.GarageTrack.System.Api.Domain.Company.Entities.Company;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database.Repositories
{
    public class CompanyRepository : RepositoryBase<CompanyEntity, long>, ICompanyRepository
    {
        public CompanyRepository(GarageTrackContext context) : base(context)
        {
        }

        public async Task<bool> AnyWithCnpjAsync(string cnpj)
        {
            return await _dbSet.AnyAsync(c => c.CNPJ == cnpj);
        }
    }
}
