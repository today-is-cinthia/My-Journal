using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Journal;
using My_Journal.Models.Ofrenda;
using My_Journal.Models.OfrendaCategoria;

namespace My_Journal.Controllers
{
    public class OfrendasCategoriasController : Controller
    {
        private readonly CbnIglesiaContext _context;

        public OfrendasCategoriasController(CbnIglesiaContext context)
        {
            _context = context;
        }

        // GET: OfrendasCategorias
        public async Task<IActionResult> Index()
        {
            try
            {
                MantOfrendaCategoria mantOfrendaCategoria = new MantOfrendaCategoria();
                var categorias = mantOfrendaCategoria.Getlistado();
                return View(categorias);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(new List<OfrendaViewModel>());
            }

            //var cbnIglesiaContext = _context.OfrendasCategorias.Include(o => o.UsuarioCreacionNavigation);
            //return View(await cbnIglesiaContext.ToListAsync());
        }

        // GET: OfrendasCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrendasCategoria = await _context.OfrendasCategorias
                .Include(o => o.UsuarioCreacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCatOfrenda == id);
            if (ofrendasCategoria == null)
            {
                return NotFound();
            }

            return View(ofrendasCategoria);
        }

        // GET: OfrendasCategorias/Create
        public IActionResult Create()
        {
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: OfrendasCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCatOfrenda,Nombre,Descripcion,Estado,UsuarioCreacion,FechaCreacion,UsuarioModifica,FechaModifica")] OfrendasCategoria ofrendasCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ofrendasCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", ofrendasCategoria.UsuarioCreacion);
            return View(ofrendasCategoria);
        }

        // GET: OfrendasCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrendasCategoria = await _context.OfrendasCategorias.FindAsync(id);
            if (ofrendasCategoria == null)
            {
                return NotFound();
            }
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", ofrendasCategoria.UsuarioCreacion);
            return View(ofrendasCategoria);
        }

        // POST: OfrendasCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCatOfrenda,Nombre,Descripcion,Estado,UsuarioCreacion,FechaCreacion,UsuarioModifica,FechaModifica")] OfrendasCategoria ofrendasCategoria)
        {
            if (id != ofrendasCategoria.IdCatOfrenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofrendasCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfrendasCategoriaExists(ofrendasCategoria.IdCatOfrenda))
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
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", ofrendasCategoria.UsuarioCreacion);
            return View(ofrendasCategoria);
        }

        // GET: OfrendasCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrendasCategoria = await _context.OfrendasCategorias
                .Include(o => o.UsuarioCreacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCatOfrenda == id);
            if (ofrendasCategoria == null)
            {
                return NotFound();
            }

            return View(ofrendasCategoria);
        }

        // POST: OfrendasCategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ofrendasCategoria = await _context.OfrendasCategorias.FindAsync(id);
            if (ofrendasCategoria != null)
            {
                _context.OfrendasCategorias.Remove(ofrendasCategoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfrendasCategoriaExists(int id)
        {
            return _context.OfrendasCategorias.Any(e => e.IdCatOfrenda == id);
        }
    }
}
