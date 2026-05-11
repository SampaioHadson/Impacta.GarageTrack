using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Domain.ParkingTax.Entities
{
    public class ParkingTax : EntityBase<long>
    {
        private ParkingTax()
        {
            Type = ParkingTaxType.Hour;
            Value = 0;
            CompanyId = 0;
        }

        public ParkingTax(ParkingTaxType type, int? minutes, int? fromHours, decimal value, long companyId)
        {
            Type = type;
            Minutes = minutes;
            FromHours = fromHours;
            Value = value;
            CompanyId = companyId;
        }

        public ParkingTaxType Type { get; set; }
        public int? Minutes { get; set; }
        public int? FromHours { get; set; }
        public decimal Value { get; set; }
        public long CompanyId { get; set; }
    }

    public enum ParkingTaxType
    {
        Hour = 1,
        AfterHour = 2,
        Daily = 3
    }
}
