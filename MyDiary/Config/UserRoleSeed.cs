using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiary.Config
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserRoleSeed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async void Seed()
        {
            if((await _roleManager.FindByNameAsync("Admin")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            }
            if ((await _roleManager.FindByNameAsync("User")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });

            }
            if((await _userManager.FindByEmailAsync("admin@gmail.com")) == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "6902341234"
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, "nhy6%TGB");
                    await _userManager.AddToRoleAsync(user, "Admin");

                }
            }
        }
    }
}
