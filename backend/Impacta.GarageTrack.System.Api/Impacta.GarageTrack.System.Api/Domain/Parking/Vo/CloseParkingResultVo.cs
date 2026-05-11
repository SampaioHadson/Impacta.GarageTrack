using Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities;

namespace Impacta.GarageTrack.System.Api.Domain.Parking.Vo
{
    public class CloseParkingResultVo
    {
        public long Id { get; set; }
        public string? Plate { get; set; }
        public string? Color { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public int TotalMinutes { get; set; }
        public List<AppliedTaxDetailVo> AppliedTaxes { get; set; } = [];
        public decimal TotalValue { get; set; }
    }

    public class AppliedTaxDetailVo
    {
        public ParkingTaxType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }
}
