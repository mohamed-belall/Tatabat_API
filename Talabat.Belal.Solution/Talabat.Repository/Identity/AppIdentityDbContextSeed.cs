using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static  class AppIdentityDbContextSeed
    {
        public static  async Task identitySeedAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "mohamed belal",
                    Email = "mohamedbelal.eng@gmail.com",
                    UserName = "MohamedBelal",
                    PhoneNumber = "+201210759564",
                };


              await   _userManager.CreateAsync(user , "Pas&&w0rd");
            }
        }
    }
}
