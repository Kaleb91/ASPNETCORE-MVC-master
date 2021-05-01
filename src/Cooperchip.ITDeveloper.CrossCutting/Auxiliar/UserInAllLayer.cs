using Cooperchip.ITDeveloper.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Cooperchip.ITDeveloper.CrossCutting.Auxiliar
{
    public class UserInAllLayer : IUserInAllLayer
    {
        private readonly IUserInContext _user;
        public UserInAllLayer(IUserInContext user)
        {
            _user = user;
        }

        public IDictionary<string, string> DictionaryOfClaims()
        {
            var email = "";

            IDictionary<string, string> MinhasClains = new Dictionary<string, string>();

            if (_user.IsAuthenticated())
            {
                //var apelido = User.FindFirst(x => x.Type == "Apelido")?.Value;
                //email = User.FindFirst(e => e.Type == "Email")?.Value;

                MinhasClains.Add("Apelido", _user.GetUserApelido());
                MinhasClains.Add("Nome Completo", _user.GetUserNomeCompleto());
                MinhasClains.Add("Imagem do Perfil", _user.GetUserImgProfilePatch());
                MinhasClains.Add("Id", _user.GetUserId().ToString());
                MinhasClains.Add("Nome", _user.Name);
                MinhasClains.Add("Email", _user.GetUserEmail());
                MinhasClains.Add("E Administrador", _user.IsInRole("Admin") ? "Sim" : "Não");

                var nome = MinhasClains["Nome"];
                email = MinhasClains["Email"];
                var EhAdministrador = MinhasClains["E Administrador"];
            }

            return MinhasClains;
        }

        public IEnumerable<Claim> ListOfClaims()
        {
            var claimsOfUser = _user.GetClaimsIdentity();
            return claimsOfUser;
        }
    }
}
