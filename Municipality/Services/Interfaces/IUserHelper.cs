using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services.Interfaces
{
    public interface IUserHelper
    {
        string GetUserName();
        int GetUserId();
    }
}
