using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;

namespace TrabajadoresPrueba.Controllers
{
    public class DistritosController : Controller
    {
        //conexion a la base de datos
        private readonly DataContext _context;

        public DistritosController(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<IActionResult> Index(int id)
        {
            var distrito = await _context.Distrito.Where(t => t.IdProvincia.Equals(id)).ToListAsync();
            var modelProvincia = await _context.Provincia.FindAsync(id);
            ViewBag.Provincia = modelProvincia;
            return View(distrito);
        }

        //create
        public IActionResult Create(int idProvincia)
        {
            var distrito = new Distrito { IdProvincia = idProvincia };
            return PartialView(distrito);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Distrito model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));s
            return RedirectToAction("Index", new { id = model.IdProvincia });

        }

        // edit
        public async Task<IActionResult> Edit(int id)
        {
            var distrito = await _context.Distrito.FindAsync(id); //select * from Distrito where PK = id
            return PartialView(distrito);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Distrito model)
        {
            var modelOld = await _context.Distrito.FindAsync(model.Id);//select * from Departamento where PK = id
            modelOld.NombreDistrito = model.NombreDistrito;
            _context.Update(modelOld);//update departamento set NombreDepartamento = model.NombreDepartamento
            await _context.SaveChangesAsync(); //commit a la base de datos
            return RedirectToAction("Index", new { id = model.IdProvincia });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var idProvincia = 0;
            var distrito = await _context.Distrito.FindAsync(id);
            if (distrito != null)
            {
                idProvincia = distrito.IdProvincia;
                _context.Remove(distrito);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = idProvincia });
        }
    }
}