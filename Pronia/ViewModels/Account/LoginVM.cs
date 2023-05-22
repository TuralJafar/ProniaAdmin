using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Pronia.ViewModels.Account
{
	public class LoginVM
	{
		[Required] 
		public string UsernameOrEmail { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool IsRemebered { get; set; }
	}
}
