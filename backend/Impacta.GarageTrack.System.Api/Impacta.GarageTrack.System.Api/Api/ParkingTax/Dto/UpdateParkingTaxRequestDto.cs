using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;

namespace Impacta.GarageTrack.System.Api.Api.ParkingTax.Dto
{
    public class UpdateParkingTaxRequestDto
    {
        public required ParkingTaxType Type { get; set; }
        public int? Minutes { get; set; }
        public required decimal Value { get; set; }
    }
}
