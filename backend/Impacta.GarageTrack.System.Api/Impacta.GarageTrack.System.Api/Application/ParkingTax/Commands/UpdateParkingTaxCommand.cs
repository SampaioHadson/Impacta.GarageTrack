using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;

namespace Impacta.GarageTrack.System.Api.Application.ParkingTax.Commands
{
    public class UpdateParkingTaxCommand : IUpdateParkingTaxCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public UpdateParkingTaxCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<ParkingTaxItemVo>> HandleAsync(Request command)
        {
            var entity = await _unityOfWork.ParkingTaxRepository.GetByIdAsync(command.Id);
            if (entity is null || !entity.IsActive)
                return Result<ParkingTaxItemVo>.Failure("Tarifa não encontrada.");

            if (entity.CompanyId != command.CompanyId)
                return Result<ParkingTaxItemVo>.Failure("Tarifa não pertence à empresa informada.");

            entity.Type = command.Type;
            entity.Minutes = command.Minutes;
            entity.Value = command.Value;
            entity.UpdatedAt = DateTime.Now;

            await _unityOfWork.ParkingTaxRepository.UpdateAsync(entity);
            await _unityOfWork.SaveChangesAsync();

            var result = await _unityOfWork.ParkingTaxRepository.ReaderGetByIdAsync(entity.Id);
            return Result<ParkingTaxItemVo>.Success(result!);
        }

        public record Request(long Id, ParkingTaxType Type, int? Minutes, decimal Value, long CompanyId);
    }

    public interface IUpdateParkingTaxCommand : ICommand<UpdateParkingTaxCommand.Request, Result<ParkingTaxItemVo>>
    {
    }
}
