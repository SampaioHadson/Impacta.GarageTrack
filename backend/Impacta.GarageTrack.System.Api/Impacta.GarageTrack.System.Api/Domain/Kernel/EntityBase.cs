namespace Impacta.GarageTrack.System.Api.Domain.Kernel
{
    public class EntityBase<T>
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
