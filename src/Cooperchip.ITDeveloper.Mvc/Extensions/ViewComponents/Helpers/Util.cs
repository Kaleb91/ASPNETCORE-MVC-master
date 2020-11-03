using Cooperchip.ITDeveloper.Data.ORM;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.ViewComponents.Helpers
{
    public static class Util
    {
        public static int TotReg(ITDeveloperDbContext ctx)
        {
            int paciente = ctx.Paciente.AsNoTracking().Count();

            if (paciente == 0)
            {
                paciente = 1;
            }

            return paciente;

        }

        public static decimal GetNumRegEstado(ITDeveloperDbContext ctx, string estado)
        {
            var pac = ctx.Paciente.AsNoTracking().Count(x => x.EstadoPaciente.Descricao.Contains(estado));

            if (pac == 0)
            {
                pac = 1;
            }

            return pac;

        }

    }
}
