using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models;
using Municipality.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Services
{

    public class UserHelperService : IUserHelper
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserHelperService(IHttpContextAccessor context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public string GetUserName()
        {
            return _userManager.GetUserName(_context.HttpContext.User);
        }

        public int GetUserId()
        {
            int.TryParse(_userManager.GetUserId(_context.HttpContext.User), out int result);
            return result;
        }
    }
}
