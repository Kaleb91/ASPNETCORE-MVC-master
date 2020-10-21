using Cooperchip.ITDeveloper.Application.Extensions;
using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class CidController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public CidController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int ? pagina, string ordenacao,string stringBusca)
        {
            const int itensPorPagina = 8;
            int numeroPagina = pagina ?? 1;

            ViewData["ordenacao"] = ordenacao;
            ViewData["filtroAtual"] = stringBusca;

            var cids = from c in _context.Cid select c;

            if (!string.IsNullOrEmpty(stringBusca))
            {
                cids = cids.Where(s => s.Codigo.Contains(stringBusca) || s.Diagnostico.Contains(stringBusca));
            }

            ViewData["OrderByInternalId"] = string.IsNullOrEmpty(ordenacao) ? "CidInternalId_desc" : "";
            ViewData["OrderByCodigo"] = ordenacao == "Codigo" ? "Codigo_desc" : "Codigo";
            ViewData["OrderByDiagnostico"] = ordenacao == "Diagnostico" ? "Diagnostico_desc" : "Diagnostico";

            if (string.IsNullOrEmpty(ordenacao)) ordenacao = "CidInternalId";

            if (ordenacao.EndsWith("_desc"))
            {
                ordenacao = ordenacao.Substring(0, ordenacao.Length - 5);
                cids = cids.OrderByDescending(x => EF.Property<object>(x, ordenacao));
            }
            else
            {
                cids = cids.OrderBy(x => EF.Property<object>(x, ordenacao));
            }

            //switch (ordenacao)
            //{
            //    case "CidInternalId_desc":
            //        cids = cids.OrderByDescending(o => o.CidInternalId);
            //        break;

            //    case "Codigo":
            //        cids = cids.OrderBy(o => o.Codigo);
            //        break;

            //    case "Codigo_desc":
            //        cids = cids.OrderByDescending(o => o.Codigo);
            //        break;

            //    case "Diagnostico":
            //        cids = cids.OrderBy(o => o.Diagnostico);
            //        break;

            //    case "Diagnostico_desc":
            //        cids = cids.OrderByDescending(o => o.Diagnostico);
            //        break;


            //    default:
            //        cids = cids.OrderBy(o => o.CidInternalId);
            //        break;

            //}

            return View(await cids.AsNoTracking().ToPagedListAsync(numeroPagina,itensPorPagina));
        }

        public IActionResult ArquivoInvalido()
        {
            TempData["ArquivoInvalido"] = "O Arquivo não é válido";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportCid(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (!ArquivoValido.IsValid(file, "Cid.Csv"))
            {
                return RedirectToAction("ArquivoInvalido");
            }
            var filepath = $"{webHostEnvironment.WebRootPath}\\importFile\\{file.FileName}";
            CopiarArquivo.Copiar(file, filepath);

            int k = 0;
            string line;

            List<Cid> cids = new List<Cid>();
            Encoding encodingPage = Encoding.GetEncoding(1252);

            using (var fs = System.IO.File.OpenRead(filepath))
            using (var stream = new StreamReader(fs, encoding: encodingPage, false))
                while((line = stream.ReadLine()) != null)
                {
                    string[] parts = line.Split(";");

                    //Pular Cabeçalho
                    if (k>0)
                    {
                        if (!_context.Cid.Any(e => e.CidInternalId == int.Parse(parts[0])))
                        {
                            cids.Add(new Cid
                            {
                                CidInternalId = int.Parse(parts[0]),
                                Codigo = parts[1],
                                Diagnostico = parts[2]
                            });                                                                                                                                                                                                                
                        }
                    }
                    k++;
                }
            if (cids.Any())
            {
                await _context.AddRangeAsync(cids);
                await _context.SaveChangesAsync();
            }
                return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var cid = await _context.Cid.FirstOrDefaultAsync(c => c.Id == id);
            if (cid == null) return BadRequest();

            return View(cid);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cid cid)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cid);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw new Exception (ex.Message);
                }
                RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cid = await _context.Cid.FindAsync(id);
            if (cid == null) return NotFound();
            return View(cid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cid cid,Guid id)
        {
            if (cid.Id != id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cid);
                    await _context.SaveChangesAsync();
                }
                catch (DBConcurrencyException)
                {
                    if (!CidExist(cid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                RedirectToAction(nameof(Index));
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var cid = await _context.Cid.FirstOrDefaultAsync(c => c.Id == id);
            if (cid.Id == null) return NotFound();

            return View(cid);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cid = _context.Cid.FindAsync(id);
            _context.Remove(cid);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CidExist(Guid id)
        {
            return _context.Cid.Any(s => s.Id == id);
        }
    }
}
