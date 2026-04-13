using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;

namespace Impacta.GarageTrack.System.Api.Domain.ParkingTax.Vo
{
    public class ParkingTaxItemVo
    {
        public long Id { get; set; }
        public ParkingTaxType Type { get; set; }
        public int? Minutes { get; set; }
        public decimal Value { get; set; }
        public long CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
