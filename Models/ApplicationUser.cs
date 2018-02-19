using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models
{

  public class ApplicationUser : IdentityUser<int>
  {
    public string Name => UserName;

    public virtual IEnumerable<IdentityUserRole<int>> Roles { get; set; }

    public virtual IEnumerable<ApplicationUserLogin> Logins { get; set; }

    public virtual IEnumerable<IdentityUserClaim<int>> Claims { get; set; }
    public IEnumerable<Incident> Incidents { get; set; }
  }
}

