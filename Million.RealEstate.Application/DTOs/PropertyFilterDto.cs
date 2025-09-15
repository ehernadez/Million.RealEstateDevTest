namespace Million.RealEstate.Application.DTOs
{
    public class PropertyFilterDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int? IdOwner { get; set; }
        public int? MinBedrooms { get; set; }
        public int? MaxBedrooms { get; set; }
        public int? MinBathrooms { get; set; }
        public int? MaxBathrooms { get; set; }
        public bool? IsActive { get; set; }
    }
}