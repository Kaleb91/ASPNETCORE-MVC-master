using Cooperchip.ITDeveloper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cooperchip.ITDeveloper.Domain.Models
{
    public class Cid : EntityBase
    {
        [Display(Name = "Internal ID")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public int CidInternalId { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(6)]
        public string Codigo { get; set; }

        [Display(Name = "Diagnóstico")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        [MaxLength(4000)]
        public string Diagnostico { get; set; }
    }
}
