using HepsiTools.DataAccess;
using HepsiTools.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HepsiTools.Helper
{
    public static class DataSeeding
    {
        public static void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {   
            DataSeeding.SeedRoles(roleManager);
            DataSeeding.SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RoleType.User.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = RoleType.User.ToString();
                roleManager.CreateAsync(role);
            }

            if (!roleManager.RoleExistsAsync(RoleType.Admin.ToString()).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = RoleType.Admin.ToString();
                roleManager.CreateAsync(role);
            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync(SeedAdmin.Email).Result == null)
            {
                User user = new User();
                user.UserName = SeedAdmin.Email;
                user.Email = SeedAdmin.Email;
                user.FirstName = SeedAdmin.Name;
                user.LastName = SeedAdmin.SurName;

                IdentityResult result = userManager.CreateAsync(user, SeedAdmin.NewPassword).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RoleType.Admin.ToString()).Wait();
                }
            }
        }

    }
}
