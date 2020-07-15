using Microsoft.AspNetCore.Identity;

namespace SS.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
    }
}
