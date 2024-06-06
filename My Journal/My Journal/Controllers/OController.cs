using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace My_Journal.Controllers
{
    public class OController : Controller
    {
        private readonly CbnIglesiaContext _context;
        public OController(CbnIglesiaContext context)
        {
            _context = context;
        }
        // GET: Miembroes
        public async Task<IActionResult> Index()
        {
            var cbnIglesiaContext = _context.OfrendasCategorias.Include(m => m.UsuarioCreacionNavigation);
            return View(await cbnIglesiaContext.ToListAsync());
        }

    }
}
