using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Journal.Models.Divisa;
using My_Journal.Models.Ofrenda;
using My_Journal.Models.OfrendaCategoria;
using My_Journal.Models.Pagos;
using My_Journal.Models.PagosCategoria;

namespace My_Journal.Controllers
{
    public class PagoesController : Controller
    {
        private readonly CbnIglesiaContext _context;

        public PagoesController(CbnIglesiaContext context)
        {
            _context = context;
        }

        // GET: Pagoes
        public async Task<IActionResult> Index()
        {
            try
            {
                MantPago mantPago = new MantPago();
                var pagos = mantPago.GetListadoPagos();
                return View(pagos);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(new List<PagosViewModel>());
            }
        }

        // GET: Pagoes/Create
        public IActionResult Create()
        {
            try
            {
                MantPagosCategoria mant = new MantPagosCategoria();
                var categorias = mant.Getlistado();
                var categoriasSelectList = new SelectList(categorias, "IdCategoria", "Nombre");

                MantDivisa mantDivisa = new MantDivisa();
                var divisa = mantDivisa.Getlistado();
                var divisaSelectList = new SelectList(divisa, "IdDivisa", "CodDivisa");


                ViewBag.ListadoPagosCategorias = categoriasSelectList;
                ViewBag.ListadoDivisa = divisaSelectList;

                var model = new PagosViewModel()
                {
                    Pagos = new Pago()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(new PagosViewModel());
            }
        }

        // POST: Pagoes/Create
        public ActionResult Crear(PagosViewModel viewModel)
        {
            try
            {
                MantPago mant = new MantPago();
                var ofrenda = mant.Insertar(viewModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(viewModel);
            }
        }
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdPago,,Descripcion,Fecha,UsuarioCreacion,FechaCreacion,UsuarioModifica,FechaModifica, Cantidad")] Pago pago)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(pago);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", pago.UsuarioCreacion);
        //    return View(pago);
        //}

        //public ActionResult Create(List<int> OfrendaCategoria, List<string> Descripcion, List<decimal> Cantidad, List<int> Divisa, List<decimal> TasaCambio, List<DateTime> Fecha)
        //{
        //    try
        //    {
        //        // Validar que todas las listas tengan la misma longitud
        //        if (OfrendaCategoria.Count == Descripcion.Count &&
        //            Descripcion.Count == Cantidad.Count &&
        //            Cantidad.Count == Divisa.Count &&
        //            Divisa.Count == TasaCambio.Count &&
        //            TasaCambio.Count == Fecha.Count)
        //        {
        //            // Crear una lista para almacenar las ofrendas
        //            var ofrendas = new List<Ofrenda>();

        //            // Iterar sobre las listas y crear objetos Ofrenda
        //            for (int i = 0; i < OfrendaCategoria.Count; i++)
        //            {
        //                var ofrenda = new Ofrenda
        //                {
        //                    IdCatOfrenda = OfrendaCategoria[i],
        //                    Descripcion = Descripcion[i],
        //                    Cantidad = (double)Cantidad[i],
        //                    Divisa = Divisa[i],
        //                    TasaCambio = (double)TasaCambio[i],
        //                    Fecha = Fecha[i]
        //                };
        //                ofrendas.Add(ofrenda);
        //            }

        //            // Insertar las ofrendas en la base de datos
        //            MantOfrenda mantOfrenda = new MantOfrenda();
        //            foreach (var ofrenda in ofrendas)
        //            {
        //                mantOfrenda.Insertar(ofrenda);
        //            }

        //            // Redirigir a la acción Index después de la inserción
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("Las listas proporcionadas tienen diferentes longitudes.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción según sea necesario
        //        // Cargar la lista de categorías para la vista en caso de error
        //        MantOfrendaCategoria mantCategoria = new MantOfrendaCategoria();
        //        var categorias = mantCategoria.Getlistado();
        //        var categoriasSelectList = new SelectList(categorias, "IdCatOfrenda", "Nombre");
        //        ViewBag.ListadoOfrendasCategorias = categoriasSelectList;

        //        // Devolver la vista con los datos ingresados y el mensaje de error
        //        ModelState.AddModelError("", "Ocurrió un error al guardar los datos: " + ex.Message);
        //        return View();
        //    }
        //}


        // GET: Pagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", pago.UsuarioCreacion);
            return View(pago);
        }

        // POST: Pagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPago,Descripcion,Fecha,UsuarioCreacion,FechaCreacion,UsuarioModifica,FechaModifica, Cantidad")] Pago pago)
        {
            if (id != pago.IdPago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(pago.IdPago))
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
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", pago.UsuarioCreacion);
            return View(pago);
        }

        // GET: Pagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pago = await _context.Pagos
                .Include(p => p.UsuarioCreacionNavigation)
                .FirstOrDefaultAsync(m => m.IdPago == id);
            if (pago == null)
            {
                return NotFound();
            }

            return View(pago);
        }

        // POST: Pagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPago == id);
        }
    }
}
