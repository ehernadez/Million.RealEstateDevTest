namespace Million.RealEstate.Application.DTOs
{
    public class PropertyTraceDto
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }
    }
}