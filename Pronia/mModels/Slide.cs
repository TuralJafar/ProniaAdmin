using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.mModels
{
    public class Slide
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [Required]
        public  string SubTitle { get; set; }   
        public string? Description { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
