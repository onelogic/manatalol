using Microsoft.AspNetCore.Identity;

namespace Manatalol.App.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
