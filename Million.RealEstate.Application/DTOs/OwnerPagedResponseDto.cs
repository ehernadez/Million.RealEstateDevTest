namespace Million.RealEstate.Application.DTOs
{
    public class OwnerPagedResponseDto
    {
        public IEnumerable<OwnerDto> Items { get; set; } = new List<OwnerDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
