using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cooperchip.ITDeveloper.Mvc.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IUnitOfUpload _unitOfUpload;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IUnitOfUpload unitOfUpload)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _unitOfUpload = unitOfUpload;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            //Todo: 
            [PersonalData]
            [Required(ErrorMessage = "O Campo {0} é Obrigátorio!")]
            [StringLength(maximumLength: 35, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres!", MinimumLength = 2)]
            public string Apelido { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "O Campo {0} é Obrigátorio!")]
            [Display(Name = "Nome Completo")]
            [StringLength(maximumLength: 80, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres!", MinimumLength = 2)]
            public string NomeCompleto { get; set; }

            [PersonalData]
            [Required(ErrorMessage = "O Campo {0} é Obrigátorio!")]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Nascimento")]
            public DateTime DataNascimento { get; set; }

            [ProtectedPersonalData]
            [DataType(DataType.Text)]
            [StringLength(maximumLength: 255, ErrorMessage = "O Campo{0} deve ter entre {2} e {1} caracteres!", MinimumLength = 21)]
            public string ImgProfilePath { get; set; }

            //===================================================================================================================================


            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Celular")]
            public string PhoneNumber { get; set; }


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Apelido = user.Apelido,
                NomeCompleto = user.NomeCompleto,
                DataNascimento = user.DataNascimento,
                ImgProfilePath = user.ImgProfilePath

            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            //Todo: By Kaleb

            if (Input.Apelido != user.Apelido)
            {
                user.Apelido = Input.Apelido;
            }
            if (Input.NomeCompleto != user.NomeCompleto)
            {
                user.NomeCompleto = Input.NomeCompleto;
            }
            if (Input.DataNascimento != user.DataNascimento)
            {
                user.DataNascimento = Input.DataNascimento;
            }


            if (file != null)
            {
                _unitOfUpload.UploadImage(file);
                user.ImgProfilePath = file.FileName;
            }
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Perfil Atualizado com sucesso!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
