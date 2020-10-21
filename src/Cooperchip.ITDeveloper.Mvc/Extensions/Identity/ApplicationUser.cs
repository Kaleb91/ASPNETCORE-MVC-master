using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required(ErrorMessage ="O Campo {0} é Obrigátorio!")]
        [StringLength(maximumLength:35,ErrorMessage ="O Campo {0} deve ter entre {2} e {1} caracteres!",MinimumLength =2)]
        public string Apelido { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "O Campo {0} é Obrigátorio!")]
        [Display(Name = "Nome Completo")]
        [StringLength(maximumLength: 80, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres!", MinimumLength = 2)]
        public string NomeCompleto { get; set; }

        [PersonalData]
        [Required(ErrorMessage = "O Campo {0} é Obrigátorio!")]
        [DataType(DataType.Date)]
        [Display(Name ="Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
