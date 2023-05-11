using System.ComponentModel.DataAnnotations;

namespace Pronia.mModels
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad hissesi bosh ola bilmez")]
        [MaxLength(25, ErrorMessage = "Uzunluq 25den cox olmamalidir")]
        public string Name { get; set; }

        public List<Product>? Products { get; set; }
    }
}
