using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;
using TrabajadoresPrueba.Models;
// va haber comandos para realizar los combos

namespace TrabajadoresPrueba.Controllers
{
    public class TrabajadoresController : Controller
    {
        //Conexion a la base de datos
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TrabajadoresController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //index TRABAJADORES
        [HttpGet]
        public IActionResult Index()
        {
            var listado = _context.Trabajadores.ToList();
            return View(listado);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });


            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento");

            var Departamentos = await _context.Departamento.ToListAsync();
            ViewData["IdDepartamento"] = new SelectList(Departamentos, "Id", "NombreDepartamento");

            /*
             ViewBag de Departamento
             ViewBag de Tipos de documento
            */
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> CargarProvincias(int id)
        {
            var listado = await _context.Provincia.Where(t => t.IdDepartamento.Equals(id)).ToListAsync();
            return Json(listado);
        }

        [HttpGet]
        public async Task<JsonResult> CargarDistritos(int id)
        {
            var listado = await _context.Distrito.Where(t => t.IdProvincia.Equals(id)).ToListAsync();
            return Json(listado);
        }

        //Para crear la ficha
        [HttpPost]
        public async Task<IActionResult> Create(Trabajadores model)
        {
            var prueba = model.FichaIFormFile;

            if(model.FichaIFormFile!=null)
            {
                model.Ficha = await CargarDocumento(model.FichaIFormFile, "Ficha");
            }

            _context.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        
        private async Task<string> CargarDocumento(IFormFile FichaIFormFile, string ruta)
        {
            var guid = Guid.NewGuid().ToString();
            var fileName = guid + Path.GetExtension(FichaIFormFile.FileName);
            //Obtengo extension del documento
            var carga1 = Path.Combine(_webHostEnvironment.WebRootPath, "images", ruta);
            /*var carga = Path.Combine(_webHostEnvironment.WebRootPath, string.Format("images\\{0}", ruta));*/
            using (var fileStream = new FileStream(Path.Combine(carga1,fileName),FileMode.Create))
            {
                await FichaIFormFile.CopyToAsync(fileStream);
            }
            return string.Format("/images/{0}/{1}", ruta, fileName);
        }
        

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Trabajadores.FindAsync(id);

            var TiposDocumentos = new List<TiposDocumentos>();
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "DNI", NombreDocumento = "DNI" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "CXE", NombreDocumento = "Carnet Extranjeria" });
            TiposDocumentos.Add(new TiposDocumentos { TipoDocumento = "PAS", NombreDocumento = "Pasaporte" });
            ViewBag.TipoDocumento = new SelectList(TiposDocumentos, "TipoDocumento", "NombreDocumento", model.TipoDocumento);

            var Departamentos = await _context.Departamento.ToListAsync();
            ViewBag.IdDepartamento = new SelectList(Departamentos, "Id", "NombreDepartamento", model.IdDepartamento);
            var Provincias = await _context.Provincia.Where(t=>t.IdDepartamento.Equals(model.IdDepartamento)).ToListAsync();
            ViewBag.IdProvincia = new SelectList(Provincias, "Id", "NombreProvincia", model.IdProvincia);
            var Distritos = await _context.Distrito.Where(t => t.IdProvincia.Equals(model.IdDistrito)).ToListAsync();
            ViewBag.IdDistrito = new SelectList(Distritos, "Id", "NombreDistrito", model.IdDistrito);

            /*
             ViewBag de Departamento
             ViewBag de Tipos de documento
            */

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Trabajadores model)
        {
            var modelOld = await _context.Trabajadores.FindAsync(model.Id);

            modelOld.TipoDocumento = model.TipoDocumento; // Actualizar el campo TipoDocumento
            modelOld.NumeroDocumento = model.NumeroDocumento; // Actualizar el campo NumeroDocumento
            modelOld.Nombres = model.Nombres; // Actualizar el campo Nombres
            modelOld.Sexo = model.Sexo; // Actualizar el campo Sexo
            modelOld.IdDepartamento = model.IdDepartamento; // Actualizar el campo IdDepartamento
            modelOld.IdProvincia = model.IdProvincia; // Actualizar el campo IdProvincia
            modelOld.IdDistrito = model.IdDistrito; // Actualizar el campo IdDistrito

            _context.Update(modelOld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        ////Delete ZONAS method GETT
        public async Task<IActionResult> Delete(int id)
        {
            var trabajadores = await _context.Trabajadores.FindAsync(id);
            _context.Remove(trabajadores);//Delete zonas
            await _context.SaveChangesAsync();//commit a la base de datos
            return RedirectToAction(nameof(Index));
        }
    }
}

