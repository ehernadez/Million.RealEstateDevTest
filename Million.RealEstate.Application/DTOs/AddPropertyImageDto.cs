namespace Million.RealEstate.Application.DTOs
{
    public class AddPropertyImageDto
    {
        public Stream ImageStream { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Length { get; set; }
        public bool Enabled { get; set; } = true;
    }
}