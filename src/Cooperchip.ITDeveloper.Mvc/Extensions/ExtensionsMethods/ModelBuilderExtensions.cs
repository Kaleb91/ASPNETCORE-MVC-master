using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.ExtensionsMethods
{
    public static class ModelBuilderExtensions 
    {
        public static ModelBuilder AddGeneric (this ModelBuilder builder)
        {
            var k = 0;
            string line;

            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outputDirectory, "Csv\\Generico.CSV");
            string filePath = new Uri(csvPath).LocalPath;

            using (var fs = File.OpenRead(filePath))
                using(var reader = new StreamReader(fs))

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    var codigo = parts[0];
                    var generico = parts[1];
                    if (k > 0)
                    {
                        builder.Entity<Generico>().HasData(
                            new Generico
                            {
                                Codigo = Convert.ToInt32(codigo),
                                Nome = generico
                            });
                    }
                    k++;
                }
            return builder;
        }

        public static ModelBuilder AddCid(this ModelBuilder builder)
        {
            var k = 0;
            string line;

            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var csvPath = Path.Combine(outputDirectory, "Csv\\Cid.CSV");
            string filePath = new Uri(csvPath).LocalPath;

            using (var fs = File.OpenRead(filePath))
            using (var reader = new StreamReader(fs))

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    var CidInternalId = parts[0];
                    var codigo = parts[1];
                    var diagnostico = parts[2];
                    if (k > 0)
                    {
                        builder.Entity<Cid>().HasData(
                            new Cid
                            {
                                CidInternalId = Convert.ToInt32(CidInternalId),
                                Codigo = codigo,
                                Diagnostico = diagnostico
                            });
                    }
                    k++;
                }
            return builder;
        }
    }
}
