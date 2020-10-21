using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cooperchip.ITDeveloper.Mvc.Areas.Identity.Pages.Account
{
    public class ConfirmarEmailLoginModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly IEmailSender _emailSender;

        public ConfirmarEmailLoginModel(UserManager<ApplicationUser> usermanger, IEmailSender emailSnder)
        {
            _usermanger = usermanger;
            _emailSender = emailSnder;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Email Inválido ou inexistente!";
                return Page();
            }

            var user = await _usermanger.FindByNameAsync(Input.Email);
            if (user == null)
            {
                return NotFound($"Não existe usuário com o email [ {Input.Email}] cadastrado!");
            }

            var userID = await _usermanger.GetUserIdAsync(user);
            var email = await _usermanger.GetEmailAsync(user);
            var code = await _usermanger.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { userID = userID, code = code }, protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(email, "Confirme seu Email antes de Logar!", $"Por gentileza,confirme sua conta " +
                $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Clicando Aqui</a>");

            StatusMessage = "Verifição de Conta enviada. Por gentiza verifique seu Email";

            if (Input.Email != user.Email)
            {
                Input.Email = user.Email;
            }

            return Page();
        }
    }
}
