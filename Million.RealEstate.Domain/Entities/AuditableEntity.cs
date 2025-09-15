namespace Million.RealEstate.Domain.Entities
{
    public class AuditableEntity
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
