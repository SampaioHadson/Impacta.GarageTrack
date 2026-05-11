using Impacta.GarageTrack.System.Api.Application.Kernel;
using Impacta.GarageTrack.System.Api.Domain.Parking.Vo;
using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;

namespace Impacta.GarageTrack.System.Api.Application.Parking.Commands
{
    public class CloseParkingCommand : ICloseParkingCommand
    {
        private readonly IUnityOfWork _unityOfWork;

        public CloseParkingCommand(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Result<CloseParkingResultVo>> HandleAsync(Request command)
        {
            var entity = await _unityOfWork.ParkingRepository.GetByIdAsync(command.Id);

            if (entity is null || !entity.IsActive)
                return Result<CloseParkingResultVo>.Failure("Registro de estacionamento não encontrado.");

            if (entity.CompanyId != command.CompanyId)
                return Result<CloseParkingResultVo>.Failure("Registro não pertence à empresa informada.");

            if (entity.ExitTime.HasValue)
                return Result<CloseParkingResultVo>.Failure("Este estacionamento já foi encerrado.");

            var exitTime = DateTime.Now;
            var totalMinutes = (int)Math.Ceiling((exitTime - entity.EntryTime).TotalMinutes);

            var taxes = (await _unityOfWork.ParkingTaxRepository.ReaderGetAllByCompanyIdAsync(command.CompanyId)).ToList();

            var dailyTax = taxes.FirstOrDefault(t => t.Type == ParkingTaxType.Daily);
            var hourTax = taxes.FirstOrDefault(t => t.Type == ParkingTaxType.Hour);
            var afterHourTax = taxes.FirstOrDefault(t => t.Type == ParkingTaxType.AfterHour);

            var appliedTaxes = new List<AppliedTaxDetailVo>();
            decimal totalValue = 0;

            if (dailyTax != null && dailyTax.FromHours.HasValue && totalMinutes >= dailyTax.FromHours.Value * 60)
            {
                totalValue = dailyTax.Value;
                appliedTaxes.Add(new AppliedTaxDetailVo
                {
                    Type = ParkingTaxType.Daily,
                    Description = $"Diária (a partir de {dailyTax.FromHours}h)",
                    Value = dailyTax.Value
                });
            }
            else if (hourTax != null)
            {
                appliedTaxes.Add(new AppliedTaxDetailVo
                {
                    Type = ParkingTaxType.Hour,
                    Description = hourTax.Minutes.HasValue
                        ? $"1ª hora (até {hourTax.Minutes} min)"
                        : "1ª hora",
                    Value = hourTax.Value
                });
                totalValue += hourTax.Value;

                if (hourTax.Minutes.HasValue && totalMinutes > hourTax.Minutes.Value && afterHourTax != null)
                {
                    var remainingMinutes = totalMinutes - hourTax.Minutes.Value;
                    var additionalHours = (int)Math.Ceiling(remainingMinutes / 60.0);
                    var additionalValue = additionalHours * afterHourTax.Value;

                    appliedTaxes.Add(new AppliedTaxDetailVo
                    {
                        Type = ParkingTaxType.AfterHour,
                        Description = $"{additionalHours} hora(s) adicional(is)",
                        Value = additionalValue
                    });
                    totalValue += additionalValue;
                }
            }

            entity.ExitTime = exitTime;
            entity.TotalValue = totalValue;
            entity.FinishedByUserId = command.UserId;
            entity.UpdatedAt = exitTime;

            await _unityOfWork.ParkingRepository.UpdateAsync(entity);
            await _unityOfWork.SaveChangesAsync();

            return Result<CloseParkingResultVo>.Success(new CloseParkingResultVo
            {
                Id = entity.Id,
                Plate = entity.Plate,
                Color = entity.Color,
                EntryTime = entity.EntryTime,
                ExitTime = exitTime,
                TotalMinutes = totalMinutes,
                AppliedTaxes = appliedTaxes,
                TotalValue = totalValue
            });
        }

        public record Request(long Id, long UserId, long CompanyId);
    }

    public interface ICloseParkingCommand : ICommand<CloseParkingCommand.Request, Result<CloseParkingResultVo>>
    {
    }
}
