using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Application.Extensions;
using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ImportarCid()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ImportarGenerico()
        {
            return View();
        }


        [HttpGet]
        public IActionResult MenuConfig()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ImportMedicamentos([FromServices] ITDeveloperDbContext context)
        {
            var filePath = ImportUtils.GetFilePath("Csv", "MedicamentosUtf8", ".CSV");

            ReadWriteFile rw = new ReadWriteFile();
            if (!await rw.ReadAndWriteCsvAsync(filePath, context))
                return View("JaTemMedicamento", context.Medicamento.AsNoTracking().OrderBy(o => o.Codigo));
            return View("ListaMedicamentos", context.Medicamento.AsNoTracking().OrderBy(o => o.Codigo));
        }
    }
}
