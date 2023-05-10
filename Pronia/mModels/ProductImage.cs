namespace Pronia.mModels
{
    public class ProductImage
    {
            public int Id { get; set; }
        public string? ImageUrl{ get; set;}
        
        public int ProcuctId { get; set;}
        public Product Product { get; set;}
        public bool? IsPrimary { get; set;}
    }
}
