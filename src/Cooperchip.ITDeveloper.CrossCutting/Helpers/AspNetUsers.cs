using Cooperchip.ITDeveloper.CrossCutting.Extensions;
using Cooperchip.ITDeveloper.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Cooperchip.ITDeveloper.CrossCutting.Helpers
{
    public class AspNetUsers : IUserInContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        ClaimsPrincipal _user;
        public AspNetUsers(IHttpContextAccessor httpContextAccessor, ClaimsPrincipal user)
        {
            _httpContextAccessor = httpContextAccessor;
            _user = user;
        }

        public string Name => _user.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_user.GetUserId()) : Guid.Empty;
        }

       

        public string GetUserApelido()
        {
            return IsAuthenticated() ? _user.GetUserApelido() : "";
        }

        public string GetUserDataNascimento()
        {
            return IsAuthenticated() ? _user.GetUserDataNascimento() : "";
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _user.GetUserEmail() : "";
            
        }



        public string GetUserImgProfilePatch()
        {
            return IsAuthenticated() ? _user.GetUserImgProfilePath() : "";
        }

        public string GetUserNomeCompleto()
        {
            return IsAuthenticated() ? _user.GetUserNomeCompleto() : "";
        }

        public bool IsAuthenticated()
        {
            return _user.Identity.IsAuthenticated;
        }
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _user.Claims;
        }
        public bool IsInRole(string role)
        {
            return _user.IsInRole(role);
        }
    }
}
