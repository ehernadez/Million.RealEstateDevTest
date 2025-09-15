namespace Million.RealEstate.Application.DTOs
{
    public class PropertyPagedResponseDto
    {
        public IEnumerable<PropertyDto> Items { get; set; } = new List<PropertyDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}