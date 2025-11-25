using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirmaAdvocacia.Data;
using FirmaAdvocacia.Models;

namespace FirmaAdvocacia.Controllers
{
    public class AdvogadoController : Controller
    {
        private readonly FirmaContext _context;

        public AdvogadoController(FirmaContext context)
        {
            _context = context;
        }

        // GET: Advogado
        public async Task<IActionResult> Index()
        {
            var advogados = await _context.Advogados
                .Include(a => a.AdvogadosProcessos)
                .ThenInclude(ap => ap.ProcessoOrigem)
                .ToListAsync();

            return View(await _context.Advogados.ToListAsync());
        }

        // GET: Advogado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var advogado = await _context.Advogados
                .Include(a => a.AdvogadosProcessos)
                .ThenInclude(ap => ap.ProcessoOrigem)
                .FirstOrDefaultAsync(m => m.AdvogadoId == id);

            if (advogado == null)
                return NotFound();

            return View(advogado);
        }

        // GET: Advogado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advogado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvogadoId,OAB,Nome,Especialidade")] Advogado advogado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advogado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(await _context.Advogados
                .Include(a => a.AdvogadosProcessos)
                .ThenInclude(ap => ap.ProcessoOrigem)
                .ToListAsync());
        }

        // GET: Advogado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advogado = await _context.Advogados.FindAsync(id);
            if (advogado == null)
            {
                return NotFound();
            }
            return View(advogado);
        }

        // POST: Advogado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvogadoId,OAB,Nome,Especialidade")] Advogado advogado)
        {
            if (id != advogado.AdvogadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advogado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvogadoExists(advogado.AdvogadoId))
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
            return View(advogado);
        }

        // GET: Advogado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advogado = await _context.Advogados
                .FirstOrDefaultAsync(m => m.AdvogadoId == id);
            if (advogado == null)
            {
                return NotFound();
            }

            return View(advogado);
        }

        // POST: Advogado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advogado = await _context.Advogados.FindAsync(id);
            if (advogado != null)
            {
                _context.Advogados.Remove(advogado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvogadoExists(int id)
        {
            return _context.Advogados.Any(e => e.AdvogadoId == id);
        }
    }
}
