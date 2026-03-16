using Impacta.GarageTrack.System.Api.Domain.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Users.Entities;

namespace Impacta.GarageTrack.System.Api.Domain.Users.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, long>
    {
        public Task<bool> AnyWithEmailAndCompanyIdAsync(string email, long companyId);
        public Task<List<User>> GetByEmailAsync(string email);
    }
}
