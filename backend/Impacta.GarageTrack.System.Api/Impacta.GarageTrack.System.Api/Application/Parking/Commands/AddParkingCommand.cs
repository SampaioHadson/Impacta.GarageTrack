using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;

namespace Impacta.GarageTrack.System.Api.Application.Parking.Commands
{
    public class AddParkingCommand : IAddParkingCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public AddParkingCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<ParkingItemVo>> HandleAsync(Request command)
        {
            var entity = new Domain.Parking.Entities.Parking(command.Plate, command.Color, DateTime.Now, command.CompanyId, command.UserId);
            await _unityOfWork.ParkingRepository.AddAsync(entity);
            await _unityOfWork.SaveChangesAsync();

            var result = await _unityOfWork.ParkingRepository.ReaderGetByIdAsync(entity.Id);
            return Result<ParkingItemVo>.Success(result!);
        }

        public record Request(string Plate, string Color, long UserId, long CompanyId);
    }

    public interface IAddParkingCommand : ICommand<AddParkingCommand.Request, Result<ParkingItemVo>>
    {
    }
}
