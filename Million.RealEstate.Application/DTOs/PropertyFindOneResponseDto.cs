namespace Million.RealEstate.Application.DTOs
{
    public class PropertyFindOneResponseDto
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
    }
}
