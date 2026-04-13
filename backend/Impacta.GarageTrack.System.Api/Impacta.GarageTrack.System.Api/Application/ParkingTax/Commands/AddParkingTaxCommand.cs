using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo;
using ParkingTaxEntity = Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities.ParkingTax;

namespace Impacta.GarageTrack.System.Api.Application.ParkingTax.Commands
{
    public class AddParkingTaxCommand : IAddParkingTaxCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public AddParkingTaxCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<ParkingTaxItemVo>> HandleAsync(Request command)
        {
            var entity = new ParkingTaxEntity(command.Type, command.Minutes, command.Value, command.CompanyId);
            await _unityOfWork.ParkingTaxRepository.AddAsync(entity);
            await _unityOfWork.SaveChangesAsync();

            var result = await _unityOfWork.ParkingTaxRepository.ReaderGetByIdAsync(entity.Id);
            return Result<ParkingTaxItemVo>.Success(result!);
        }

        public record Request(ParkingTaxType Type, int? Minutes, decimal Value, long CompanyId);
    }

    public interface IAddParkingTaxCommand : ICommand<AddParkingTaxCommand.Request, Result<ParkingTaxItemVo>>
    {
    }
}
