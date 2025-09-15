namespace Million.RealEstate.Domain.Entities
{
    public class Owner : AuditableEntity
    {
        public int IdOwner { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }

        public List<Property> Properties { get; set; } = new();
    }
}
