namespace Million.RealEstate.Domain.Entities
{
    public class PropertyImage : AuditableEntity
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public Property? Property { get; set; }
    }
}
