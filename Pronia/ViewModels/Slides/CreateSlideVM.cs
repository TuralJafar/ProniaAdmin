using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.ViewModels
{
    public class CreateSlideVM
    {
        [Required]
        public string Title { get; set; }
        
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        [Required(ErrorMessage ="Slide-da shekil olmalidir") ]
        public IFormFile Photo { get; set; }
    }
}
