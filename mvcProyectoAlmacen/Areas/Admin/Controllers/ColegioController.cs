using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;

namespace mvcProyectoColegio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrador")]
    public class ColegioController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ColegioController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        //metodo para ir a la vista index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //metodo para ir a la vista create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //metodo para guardar los datos del nuevo Colegio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Colegio Colegio)
        {
            if (ModelState.IsValid)
            {
                //logica para guardar en bd
                _contenedorTrabajo.Colegio.Add(Colegio);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View(Colegio);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Colegio Colegio = new Colegio();
            Colegio = _contenedorTrabajo.Colegio.Get(id);
            if (Colegio == null)
            {
                return NotFound();

            }
            return View(Colegio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Colegio Colegio)
        {

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Colegio.Update(Colegio);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(Colegio);
        }
        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult RegistroDeColegio()
        {
            return View();
        }


        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Colegio.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Colegio.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error borrando Colegio" });
            }
            _contenedorTrabajo.Colegio.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borro la Colegio" });
        }
        #endregion
    }
}
