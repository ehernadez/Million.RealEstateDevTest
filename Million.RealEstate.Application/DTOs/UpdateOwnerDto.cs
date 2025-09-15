namespace Million.RealEstate.Application.DTOs
{
    public class UpdateOwnerDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
    }
}