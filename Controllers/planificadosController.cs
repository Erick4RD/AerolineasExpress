using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AerolineaExpress.Models;

namespace AerolineaExpress.Controllers
{
    public class planificadosController : Controller
    {
        private readonly MyDbContext _context;

        public planificadosController(MyDbContext context)
        {
            _context = context;
        }

  
        public async Task<IActionResult> Index()
        {
            return View(await _context.planificados.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planificados = await _context.planificados
                .FirstOrDefaultAsync(m => m.VuelosId == id);
            if (planificados == null)
            {
                return NotFound();
            }

            return View(planificados);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VuelosId,Destino,Hora_de_salida,Hora_de_llegada,Cantida_de_pasajeros")] planificados planificados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planificados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planificados);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planificados = await _context.planificados.FindAsync(id);
            if (planificados == null)
            {
                return NotFound();
            }
            return View(planificados);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VuelosId,Destino,Hora_de_salida,Hora_de_llegada,Cantida_de_pasajeros")] planificados planificados)
        {
            if (id != planificados.VuelosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planificados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!planificadosExists(planificados.VuelosId))
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
            return View(planificados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planificados = await _context.planificados
                .FirstOrDefaultAsync(m => m.VuelosId == id);
            if (planificados == null)
            {
                return NotFound();
            }

            return View(planificados);
        }

        // POST: planificados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planificados = await _context.planificados.FindAsync(id);
            _context.planificados.Remove(planificados);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool planificadosExists(int id)
        {
            return _context.planificados.Any(e => e.VuelosId == id);
        }
    }
}
