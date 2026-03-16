using Impacta.GarageTrack.System.Api.Domain.Kernel;

namespace Impacta.GarageTrack.System.Api.Domain.Parking.Entities
{
    public class Parking : EntityBase<long>
    {
        private Parking()
        {
            Plate = string.Empty;
            Color = string.Empty;
            EntryTime = DateTime.UtcNow;
            CompanyId = 0;
            CreatedByUserId = 0;
        }

        public Parking(string plate, string color, DateTime entryTime, long companyId, long createdByUserId)
        {
            Plate = plate;
            Color = color;
            EntryTime = entryTime;
            CompanyId = companyId;
            CreatedByUserId = createdByUserId;
        }

        public string Plate { get; set; }
        public string Color { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public long CompanyId { get; set; }
        public long CreatedByUserId { get; set; }
        public Domain.Users.Entities.User? CreatedByUser { get; set; }
        public long? FinishedByUserId { get; set; }
        public Domain.Users.Entities.User? FinishedByUser { get; set; }
    }
}
