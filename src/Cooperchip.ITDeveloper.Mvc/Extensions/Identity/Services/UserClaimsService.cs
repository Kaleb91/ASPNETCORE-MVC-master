using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.ExtensionsMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services
{
    public class UserClaimsService : UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>
    {
        private readonly ApplicationDbContext _dbcontext;

        public UserClaimsService(ApplicationDbContext dbcontext,UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,IOptions<IdentityOptions> aoptionsAccessor)
                                  :base(userManager,roleManager,aoptionsAccessor)
        {
            _dbcontext = dbcontext;
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            //Todo: Aqui podemos pegar  as claims do user no banco de dados
            //add Claims

            ((ClaimsIdentity)principal.Identity).AddClaims(new []
            {
                new Claim("Apelido",user.Apelido),
                new Claim("NomeCompleto",user.NomeCompleto),
                new Claim("Email",user.Email),
                new Claim("DataNascimento",user.DataNascimento.ToBrazilianDate()),
                new Claim("ImgProfilePath",!string.IsNullOrEmpty(user.ImgProfilePath) ? user.ImgProfilePath : "")
            });

            return principal;
        }
    }
}
