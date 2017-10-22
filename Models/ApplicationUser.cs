using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models
{

    public class ApplicationUser : IdentityUser<int>
    {
        public string Password { get; set; }

        public ApplicationUser() { }
        public ApplicationUser(string password)
        {
            this.Password = password;
        }
        public IEnumerable<Incident> Incidents { get; set; }
    }
}

