using Impacta.GarageTrack.System.Api.Application.Kernel;

namespace Impacta.GarageTrack.System.Api.Application.Company.Commands
{
    public class AddCompanyCommand : IAddCompanyCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public AddCompanyCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<long>> HandleAsync(Request command)
        {
            if (await _unityOfWork.CompanyRepository.AnyWithCnpjAsync(command.Cnpj))
            {
                return Result<long>.Failure("A company with the same CNPJ already exists.");
            }

            using var transaction = await _unityOfWork.StartTransactionAsync();
            try
            {
                var company = new Domain.Company.Entities.Company(command.Name, command.Cnpj);
                await _unityOfWork.CompanyRepository.AddAsync(company);
                await _unityOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return Result<long>.Success(company.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<long>.Failure("An error occurred while adding the company.");
            }
        }

        public record struct Request(string Name, string Cnpj);
    }

    public interface IAddCompanyCommand : ICommand<AddCompanyCommand.Request, Result<long>>
    {
    }
}
