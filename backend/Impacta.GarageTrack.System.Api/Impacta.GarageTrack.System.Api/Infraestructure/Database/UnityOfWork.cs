using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Company.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Parking.Repositories;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Repositories;
using Impacta.GarageTrack.System.Api.Domain.Users.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Impacta.GarageTrack.System.Api.Infraestructure.Database
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly GarageTrackContext _context;

        public ICompanyRepository CompanyRepository { get; }
        public IUserRepository UserRepository { get; }
        public IParkingRepository ParkingRepository { get; }
        public IParkingTaxRepository ParkingTaxRepository { get; }

        public UnityOfWork(
            GarageTrackContext context,
            ICompanyRepository companyRepository,
            IUserRepository userRepository,
            IParkingRepository parkingRepository,
            IParkingTaxRepository parkingTaxRepository)
        {
            _context = context;
            CompanyRepository = companyRepository;
            UserRepository = userRepository;
            ParkingRepository = parkingRepository;
            ParkingTaxRepository = parkingTaxRepository;
        }

        public async Task<IDbContextTransaction> StartTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
