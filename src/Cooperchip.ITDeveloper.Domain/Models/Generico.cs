using Cooperchip.ITDeveloper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cooperchip.ITDeveloper.Domain.Models
{
    public class Generico : EntityBase
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
