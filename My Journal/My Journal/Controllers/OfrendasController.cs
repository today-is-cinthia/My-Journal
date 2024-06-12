using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Journal.Models.Divisa;
using My_Journal.Models.Ofrenda;
using My_Journal.Models.OfrendaCategoria;

namespace My_Journal.Controllers
{
    public class OfrendasController : Controller
    {
        private readonly CbnIglesiaContext _context;

        public OfrendasController(CbnIglesiaContext context)
        {
            _context = context;
        }

        // GET: Ofrendas
        public async Task<IActionResult> Index()
        {
            try
            {
                MantOfrenda mantOfrenda = new MantOfrenda();
                var ofrendas = mantOfrenda.GetListadoOfrendas();
                return View(ofrendas);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(new List<OfrendaViewModel>());
            }
        }

        // GET: Ofrendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrenda = await _context.Ofrendas
                .Include(o => o.UsuarioCreacionNavigation)
                .FirstOrDefaultAsync(m => m.IdOfrenda == id);
            if (ofrenda == null)
            {
                return NotFound();
            }

            return View(ofrenda);
        }

        // GET: Ofrendas/Create
        public IActionResult Create()
        {
            try
            {
                MantOfrendaCategoria mant = new MantOfrendaCategoria();
                var categorias = mant.Getlistado();
                var categoriasSelectList = new SelectList(categorias, "IdCatOfrenda", "Nombre");

                MantDivisa mantDivisa = new MantDivisa();
                var divisa = mantDivisa.Getlistado();
                var divisaSelectList = new SelectList(divisa, "IdDivisa", "CodDivisa");

                ViewBag.ListadoOfrendasCategorias = categoriasSelectList;
                ViewBag.ListadoDivisa = divisaSelectList;

                var model = new OfrendaViewModel
                {
                    Ofrenda = new Ofrenda()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                return View(new OfrendaViewModel());
            }
        }

        // POST: Ofrendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(OfrendaViewModel ofrendaViewModel)
        //{
        //    try
        //    {
        //        MantOfrendaCategoria mant = new MantOfrendaCategoria();
        //        var categorias = mant.Getlistado();
        //        var categoriasSelectList = new SelectList(categorias, "IdCatOfrenda", "Nombre");
        //        ViewBag.ListadoOfrendasCategorias = categoriasSelectList;

        //        new MantOfrenda().Insertar(ofrendaViewModel);

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción según sea necesario
        //        return View(ofrendaViewModel);
        //    }
        //}

        [HttpPost]
        [HttpPost]
        public ActionResult Create(List<int> OfrendaCategoria,List<string> Descripcion,List<decimal> Cantidad,List<int> Divisa,List<decimal> TasaCambio,List<DateTime> Fecha)
        {
            try
            {
                // Validar que todas las listas tengan la misma longitud
                if (OfrendaCategoria.Count == Descripcion.Count &&
                    Descripcion.Count == Cantidad.Count &&
                    Cantidad.Count == Divisa.Count &&
                    Divisa.Count == TasaCambio.Count &&
                    TasaCambio.Count == Fecha.Count)
                {
                    // Crear una lista para almacenar las ofrendas
                    var ofrendas = new List<Ofrenda>();

                    // Iterar sobre las listas y crear objetos Ofrenda
                    for (int i = 0; i < OfrendaCategoria.Count; i++)
                    {
                        var ofrenda = new Ofrenda
                        {
                            IdCatOfrenda = OfrendaCategoria[i],
                            Descripcion = Descripcion[i],
                            Cantidad = (double)Cantidad[i],
                            Divisa = Divisa[i],
                            TasaCambio = (double)TasaCambio[i],
                            Fecha = Fecha[i]
                        };
                        ofrendas.Add(ofrenda);
                    }

                    // Insertar las ofrendas en la base de datos
                    MantOfrenda mantOfrenda = new MantOfrenda();
                    foreach (var ofrenda in ofrendas)
                    {
                        mantOfrenda.Insertar(ofrenda);
                    }

                    // Redirigir a la acción Index después de la inserción
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new InvalidOperationException("Las listas proporcionadas tienen diferentes longitudes.");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario
                // Cargar la lista de categorías para la vista en caso de error
                MantOfrendaCategoria mantCategoria = new MantOfrendaCategoria();
                var categorias = mantCategoria.Getlistado();
                var categoriasSelectList = new SelectList(categorias, "IdCatOfrenda", "Nombre");
                ViewBag.ListadoOfrendasCategorias = categoriasSelectList;

                // Devolver la vista con los datos ingresados y el mensaje de error
                ModelState.AddModelError("", "Ocurrió un error al guardar los datos: " + ex.Message);
                return View();
            }
        }




        // GET: Ofrendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrenda = await _context.Ofrendas.FindAsync(id);
            if (ofrenda == null)
            {
                return NotFound();
            }
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", ofrenda.UsuarioCreacion);
            return View(ofrenda);
        }

        // POST: Ofrendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOfrenda,Cantidad,Descripcion,Fecha,UsuarioCreacion,FechaCreacion,UsuarioModifica,FechaModifica")] Ofrenda ofrenda)
        {
            if (id != ofrenda.IdOfrenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofrenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfrendaExists(ofrenda.IdOfrenda))
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
            ViewData["UsuarioCreacion"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", ofrenda.UsuarioCreacion);
            return View(ofrenda);
        }

        // GET: Ofrendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ofrenda = await _context.Ofrendas
                .Include(o => o.UsuarioCreacionNavigation)
                .FirstOrDefaultAsync(m => m.IdOfrenda == id);
            if (ofrenda == null)
            {
                return NotFound();
            }

            return View(ofrenda);
        }

        // POST: Ofrendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ofrenda = await _context.Ofrendas.FindAsync(id);
            if (ofrenda != null)
            {
                _context.Ofrendas.Remove(ofrenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfrendaExists(int id)
        {
            return _context.Ofrendas.Any(e => e.IdOfrenda == id);
        }

        //public ActionResult ListadoOfrendasCategorias()
        //{
        //    MantOfrendaCategoria mant = new MantOfrendaCategoria();
        //    return PartialView("~/Views/Ofrendas/Create.chtml", mant.Getlistado());
        //}

        //public List<OfrendasCategoria> ListadoOfrendasCategorias()
        //{
        //    MantOfrendaCategoria mant = new MantOfrendaCategoria();
        //    var categorias = mant.Getlistado();

        //    // Crear SelectList
        //    var categoriasSelectList = new SelectList(categorias, "IdCatOfrenda", "Nombre");
        //    ViewBag.ListadoOfrendasCategorias = categoriasSelectList;

        //    return View("~/Views/Ofrendas/Create.cshtml");
        //}
        public ActionResult ListadoDivisas()
        {
            MantDivisa mant = new MantDivisa();
            return PartialView("~/Views/Ofrendas/Create.cshtml", mant.Getlistado());
        }
    }
}
