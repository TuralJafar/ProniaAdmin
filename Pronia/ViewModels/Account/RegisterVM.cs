using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Pronia.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3,ErrorMessage ="Adiniz uzunlugu minumum 3 simvoldan ibaret olmalidir")]
        [MaxLength(25)]

        public string Name { get; set; }
        [MinLength(3, ErrorMessage = "Soyadiniz minumum 3 simvoldan ibaret olmalidir")]
        [MaxLength(25)]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }
        [Required]
       
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(ConfirmPassword))]
        [MinLength(8)]
       
        public string ConfirmPassword{ get; set; }
       
        public string Gender { get; set; }

    }
}
