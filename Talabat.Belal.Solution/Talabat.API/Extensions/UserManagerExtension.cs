using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.API.Extensions
{
    public  static class UserManagerExtension
    {
        public  static async Task<AppUser> FindUserAddressByEmailAsync(this UserManager<AppUser>  userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user =  await userManager.Users.Include(U => U.Address).SingleAsync(U => U.Email == email);

            return user;     

        }
    }
}
