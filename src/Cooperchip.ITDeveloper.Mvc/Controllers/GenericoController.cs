using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class GenericoController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public GenericoController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        // GET: Generico
        public async Task<IActionResult> Index()
        {
            var generico = await _context.Generico.OrderBy(g => g.Codigo).AsNoTracking().ToListAsync();
            return View(generico);
        }

        // GET: Generico/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generico = await _context.Generico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generico == null)
            {
                return NotFound();
            }

            return View(generico);
        }

        // GET: Generico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Id")] Generico generico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generico);
        }

        // GET: Generico/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generico = await _context.Generico.FindAsync(id);
            if (generico == null)
            {
                return NotFound();
            }
            return View(generico);
        }

        // POST: Generico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Codigo,Nome,Id")] Generico generico)
        {
            if (id != generico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenericoExists(generico.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(generico);
        }

        // GET: Generico/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generico = await _context.Generico
                .FirstOrDefaultAsync(m => m.Id == id);
            if (generico == null)
            {
                return NotFound();
            }

            return View(generico);
        }

        // POST: Generico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var generico = await _context.Generico.FindAsync(id);
            _context.Generico.Remove(generico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenericoExists(Guid id)
        {
            return _context.Generico.Any(e => e.Id == id);
        }
    }
}
