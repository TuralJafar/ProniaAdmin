using Microsoft.AspNetCore.Identity;

namespace Pronia.mModels
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsReminded { get; set; }
    }
}
