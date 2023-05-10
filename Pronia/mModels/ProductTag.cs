namespace Pronia.mModels
{
    public class ProductTag
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public Product Product { get; set; }
        public Tag Tag { get; set; }

    }
}
