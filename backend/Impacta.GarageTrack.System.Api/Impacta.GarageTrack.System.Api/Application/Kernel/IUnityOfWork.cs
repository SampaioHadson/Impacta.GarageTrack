using Impacta.GarageTrack.System.Api.Domain.Company.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Parking.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Impacta.GarageTrack.System.Api.Application.Kernel
{
    public interface IUnityOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IUserRepository UserRepository { get; }
        IParkingRepository ParkingRepository { get; }
        Task<IDbContextTransaction> StartTransactionAsync();
        Task SaveChangesAsync();
    }
}
