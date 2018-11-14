using Microsoft.AspNetCore.Identity;

namespace AspNetCore21MvcIdentity.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
