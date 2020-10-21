using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Identity.Services
{
    public static class DefaultUsersAndRoles
    {
        public static async Task Seed(ApplicationDbContext context ,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            const string nomecompleto = "Maria de Jesus";
            const string apelido = "Mary";
            var datanascimento = DateTime.Now;
            const string email = "mary@gmail.com";
            const string password = "Admin@123";
            const string roleName = "Admin";
            const string Username = email;

            context.Database.Migrate();

            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            if (await userManager.FindByNameAsync(Username) == null)
            {
                var user = new ApplicationUser
                {
                    Apelido = apelido,
                    NomeCompleto = nomecompleto,
                    DataNascimento = datanascimento,
                    UserName = Username,
                    Email = email,
                    PhoneNumber = "61991919094",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }

            
        }
    }
}
