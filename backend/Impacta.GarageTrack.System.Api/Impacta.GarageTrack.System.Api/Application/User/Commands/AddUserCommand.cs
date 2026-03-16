using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Application.User.Commands
{
    public class AddUserCommand : IAddUserCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public AddUserCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<long>> HandleAsync(Request command)
        {
            if (await _unityOfWork.UserRepository.AnyWithEmailAndCompanyIdAsync(command.Email, command.CompanyId))
            {
                return Result<long>.Failure("An user with email already registered at company");
            }

            using var transaction = await _unityOfWork.StartTransactionAsync();
            try
            {
                var user = new Domain.Users.Entities.User(command.Name, command.Email, command.Password, command.CompanyId, command.UserRole);
                await _unityOfWork.UserRepository.AddAsync(user);
                await _unityOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
                return Result<long>.Success(user.Id);
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<long>.Failure($"An error occurred while adding user");
            }
        }

        public record struct Request(string Name, string Email, string Password, long CompanyId, UserRole UserRole);
    }

    public interface IAddUserCommand : ICommand<AddUserCommand.Request, Result<long>>
    {
    }
}
