using Impacta.GarageTrack.System.Api.Application.Kernel;

namespace Impacta.GarageTrack.System.Api.Application.ParkingTax.Commands
{
    public class DeleteParkingTaxCommand : IDeleteParkingTaxCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public DeleteParkingTaxCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result> HandleAsync(Request command)
        {
            var entity = await _unityOfWork.ParkingTaxRepository.GetByIdAsync(command.Id);
            if (entity is null || !entity.IsActive)
                return Result.Failure("Tarifa não encontrada.");

            if (entity.CompanyId != command.CompanyId)
                return Result.Failure("Tarifa não pertence à empresa informada.");

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.Now;

            await _unityOfWork.ParkingTaxRepository.UpdateAsync(entity);
            await _unityOfWork.SaveChangesAsync();

            return Result.Success();
        }

        public record Request(long Id, long CompanyId);
    }

    public interface IDeleteParkingTaxCommand : ICommand<DeleteParkingTaxCommand.Request, Result>
    {
    }
}
