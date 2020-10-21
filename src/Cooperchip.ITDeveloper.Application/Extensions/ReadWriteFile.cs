using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Application.Extensions
{
    public class ReadWriteFile
    {
        public ReadWriteFile() { }

        public async Task<bool> ReadAndWriteCsvAsync(string filePath, ITDeveloperDbContext ctx)
        {

            var k = 0;
            string line;

            using (var fs = System.IO.File.OpenRead(filePath))
            using (var reader = new StreamReader(fs))
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(";");

                    //pular cabeçalho
                    if (k > 0)
                    {
                        var codigomedicamento = parts[0];
                        var descricao = parts[1];
                        var generico = parts[2];
                        var codigogenerico = parts[3];

                        if (JaTemMedicamento(codigomedicamento, ctx)) return false;
                        

                        

                        ctx.Add(new Medicamento
                        {   
                            Codigo = int.Parse(codigomedicamento),
                            CodigoGenerico = int.Parse(codigogenerico),
                            Descricao = descricao,
                            Generico = generico
                        });
                    }

                    k++;
                }
            await ctx.SaveChangesAsync();

            return true;
        }

        private bool JaTemMedicamento(string codigomedicamento, ITDeveloperDbContext ctx)
        {
            return ctx.Medicamento.Any(e => e.Codigo == int.Parse(codigomedicamento));
        }
    }
}