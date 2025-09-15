namespace Million.RealEstate.Application.DTOs
{
    public class CreatePropertyTraceDto
    {
        public DateTime DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal? Tax { get; set; }
    }
}