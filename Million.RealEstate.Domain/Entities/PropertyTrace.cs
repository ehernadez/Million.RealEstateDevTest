namespace Million.RealEstate.Domain.Entities
{
    public class PropertyTrace : AuditableEntity
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal? Tax { get; set; }
        public int IdProperty { get; set; }

        public Property? Property { get; set; }
    }
}
