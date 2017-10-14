using Microsoft.AspNetCore.Identity;

namespace Models
{

    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }

        public ApplicationUser() { }
        public ApplicationUser(string password)
        {
            this.Password = password;
        }
    }
}

