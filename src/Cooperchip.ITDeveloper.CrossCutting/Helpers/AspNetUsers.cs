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

        public AspNetUsers(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_httpContextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

       

        public string GetUserApelido()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserApelido() : "";
        }

        public string GetUserDataNascimento()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserDataNascimento() : "";
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserEmail() : "";
            
        }



        public string GetUserImgProfilePatch()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserImgProfilePath() : "";
        }

        public string GetUserNomeCompleto()
        {
            return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserNomeCompleto() : "";
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }
        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(role);
        }
    }
}
