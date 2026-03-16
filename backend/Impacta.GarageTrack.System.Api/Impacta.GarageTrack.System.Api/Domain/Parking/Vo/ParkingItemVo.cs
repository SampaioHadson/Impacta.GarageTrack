namespace Impacta.GarageTrack.System.Api.Domain.Parking.Vo
{
    public class ParkingItemVo
    {
        public long Id { get; set; }
        public string? Plate { get; set; }
        public string? Color { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public long CreatedByUserId { get; set; }
        public string? CreatedByUserName { get; set; }
        public long? FinishedByUserId { get; set; }
        public string? FinishedByUserName { get; set; }
    }
}
