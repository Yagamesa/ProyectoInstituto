using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcProyectoAlmacen.Data.Repository;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;
using System;
using System.Linq;

namespace mvcProyectoAlmacen.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrador")]
    public class MateriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public MateriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo ?? throw new ArgumentNullException(nameof(contenedorTrabajo));
        }

        // Acción para mostrar la lista de materias con paginación
        public IActionResult Index(int pagina = 1)
        {
            const int elementosPorPagina = 10;

            var materias = _contenedorTrabajo.Materia.GetAllMaterias();

            var totalMaterias = materias.Count();
            var totalPaginas = (int)Math.Ceiling(totalMaterias / (double)elementosPorPagina);

            var materiasPaginadas = materias.Skip((pagina - 1) * elementosPorPagina)
                                           .Take(elementosPorPagina)
                                           .ToList();

            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.PaginaActual = pagina;

            return View(materiasPaginadas);
        }

        // Acción para mostrar el formulario de creación de materia
        public IActionResult Create()
        {
            return View();
        }

        // Acción para procesar el formulario de creación de materia
        [HttpPost]
        public IActionResult Create(Materia materia)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Materia.CreateMateria(materia);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(materia);
        }

        // Acción para mostrar los detalles de una materia específica
        public IActionResult Details(int id)
        {
            var materia = _contenedorTrabajo.Materia.GetMateria(id);
            if (materia == null)
            {
                return NotFound();
            }
            return View(materia);
        }

        // Acción para mostrar el formulario de edición de materia
        public IActionResult Edit(int id)
        {
            var materia = _contenedorTrabajo.Materia.GetMateria(id);
            if (materia == null)
            {
                return NotFound();
            }
            return View(materia);
        }

        // Acción para procesar el formulario de edición de materia
        [HttpPost]
        public IActionResult Edit(int id, Materia materia)
        {
            if (id != materia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Materia.UpdateMateria(materia);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(materia);
        }

        // Acción para eliminar una materia
        public IActionResult Delete(int id)
        {
            var materia = _contenedorTrabajo.Materia.GetMateria(id);
            if (materia == null)
            {
                return NotFound();
            }
            return View(materia);
        }

        // Acción para confirmar la eliminación de una materia
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var materia = _contenedorTrabajo.Materia.GetMateria(id);
            if (materia == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Materia.DeleteMateria(id);
            _contenedorTrabajo.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetAllMaterias()
        {
            var materias = _contenedorTrabajo.Materia.GetAllMaterias();
            return Json(materias);
        }

        public IActionResult BuscarPorID(string ci)
        {
            try
            {
                // Obtener registros por CI utilizando el repositorio
                List<Materia> materias = _contenedorTrabajo.Materia.BuscarPorID(ci);

                // Verificar si hay registros y si el ID existe
                if (materias != null && materias.Any() && materias.Any(m => m.Id == int.Parse(ci)))
                {
                    return View("Index", materias);
                }
                else
                {
                    // Si no se encontraron registros o el ID no existe, mostrar un mensaje en la vista Index
                    ViewData["Mensaje"] = "No se encontraron registros para el CI especificado.";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                // En caso de error, devolver un JSON indicando el error
                return Json(new { success = false, message = "Error al buscar registros por CI." });
            }
        }

    }
}
