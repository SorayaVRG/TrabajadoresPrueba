using Microsoft.AspNetCore.Mvc;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;

namespace TrabajadoresPrueba.Controllers
{
    public class DepartamentosController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;

        public DepartamentosController(DataContext dataContext)
        {
            _context = dataContext;
        }

        //GET : /<Controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var departamentos = _context.Departamento.ToList();
            return View(departamentos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // edit
        public async Task<IActionResult> Edit(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id); //select * from Departamento where PK = id
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento model)
        {
            var modelOld = await _context.Departamento.FindAsync(model.Id);//select * from Departamento where PK = id
            modelOld.NombreDepartamento = model.NombreDepartamento;
            _context.Update(modelOld);//update departamento set NombreDepartamento = model.NombreDepartamento
            await _context.SaveChangesAsync(); //commit a la base de datos
            return RedirectToAction(nameof(Index));
        }
        //method GETT
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _context.Departamento.FindAsync(id);
            _context.Remove(departamento);//Delete del departamento
            await _context.SaveChangesAsync();//commit a la base de datos
            return RedirectToAction(nameof(Index));
        }
    }
}

