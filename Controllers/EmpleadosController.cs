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
    public class EmpleadosController : Controller
    {
        private readonly MyDbContext Empl;

        public EmpleadosController(MyDbContext context)
        {
            Empl = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await Empl.empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await Empl.empleados
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Nombre,Apellidos,Departamento,puesto")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                Empl.Add(empleados);
                await Empl.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await Empl.empleados.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }
            return View(empleados);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombre,Apellidos,Departamento,puesto")] Empleados empleados)
        {
            if (id != empleados.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Empl.Update(empleados);
                    await Empl.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosExists(empleados.EmpleadoId))
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
            return View(empleados);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleados = await Empl.empleados
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleados = await Empl.empleados.FindAsync(id);
            Empl.empleados.Remove(empleados);
            await Empl.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadosExists(int id)
        {
            return Empl.empleados.Any(e => e.EmpleadoId == id);
        }
    }
}
