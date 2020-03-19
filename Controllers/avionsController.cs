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
    public class avionsController : Controller
    {
        private readonly MyDbContext Avio;

        public avionsController(MyDbContext context)
        {
            Avio = context;
        }

  
        public async Task<IActionResult> Index()
        {
            return View(await Avio.avions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avion = await Avio.avions
                .FirstOrDefaultAsync(m => m.AvionId == id);
            if (avion == null)
            {
                return NotFound();
            }

            return View(avion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvionId,Modelo,Tipo,Capacidad,Estado")] avion avion)
        {
            if (ModelState.IsValid)
            {
                Avio.Add(avion);
                await Avio.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avion);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avion = await Avio.avions.FindAsync(id);
            if (avion == null)
            {
                return NotFound();
            }
            return View(avion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvionId,Modelo,Tipo,Capacidad,Estado")] avion avion)
        {
            if (id != avion.AvionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Avio.Update(avion);
                    await Avio.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!avionExists(avion.AvionId))
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
            return View(avion);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avion = await Avio.avions
                .FirstOrDefaultAsync(m => m.AvionId == id);
            if (avion == null)
            {
                return NotFound();
            }

            return View(avion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avion = await Avio.avions.FindAsync(id);
            Avio.avions.Remove(avion);
            await Avio.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool avionExists(int id)
        {
            return Avio.avions.Any(e => e.AvionId == id);
        }
    }
}
