namespace UltraPlay.Domain.Common
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}