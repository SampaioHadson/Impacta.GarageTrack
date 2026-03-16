using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Domain.Company.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Entities.Company, long>
    {
        Task<bool> AnyWithCnpjAsync(string cnpj);
    }
}
