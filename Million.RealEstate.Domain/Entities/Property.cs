namespace Million.RealEstate.Domain.Entities
{
    public class Property : AuditableEntity
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int? IdOwner { get; set; }

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }

        public Owner? Owner { get; set; }
        public List<PropertyImage> Images { get; set; } = new();
        public List<PropertyTrace> Traces { get; set; } = new();
    }
}
