using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;

namespace TrabajadoresPrueba.Controllers
{
    public class ProvinciasController : Controller
    {
        //conexion a la base de datos
        private readonly DataContext _context;

        public ProvinciasController(DataContext dataContext)
        {
            _context = dataContext;
        }
        //Referencia para distritos
        public async Task<IActionResult> Index(int idDepartamento)
        {
            var provincias = await _context.Provincia.Where(t => t.IdDepartamento.Equals(idDepartamento)).ToListAsync();
            var modelDepartamento = await _context.Departamento.FindAsync(idDepartamento);
            ViewBag.Departamento = modelDepartamento;
            return View(provincias);
        }


        //create
        public IActionResult Create(int idDepartamento)
        {
            var provincia = new Provincia { IdDepartamento = idDepartamento };
            return View(provincia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Provincia model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", new { id = model.IdDepartamento });

        }

        // edit
        public async Task<IActionResult> Edit(int id)
        {
            var provincia = await _context.Provincia.FindAsync(id); //select * from Departamento where PK = id
            return View(provincia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Provincia model)
        {
            var modelOld = await _context.Provincia.FindAsync(model.Id);//select * from Departamento where PK = id
            modelOld.NombreProvincia = model.NombreProvincia;
            _context.Update(modelOld);//update departamento set NombreDepartamento = model.NombreDepartamento
            await _context.SaveChangesAsync(); //commit a la base de datos
            return RedirectToAction("Index", new { id = model.IdDepartamento });
        }
        public async Task<IActionResult> Delete(int id)
        {
            var idDepartamento = 0;
            var provincia = await _context.Provincia.FindAsync(id);
            if (provincia != null) 
            {
                idDepartamento = provincia.IdDepartamento;
                _context.Remove(provincia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = idDepartamento });
        }
    }
}