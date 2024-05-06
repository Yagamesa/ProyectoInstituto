using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcProyectoAlmacen.Data.Repository;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;
using System;
using System.Collections.Generic;

namespace mvcProyectoAlmacen.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrador, Cliente")]
    public class RegistroController : Controller
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public RegistroController(IRegistroRepository registroRepository, IContenedorTrabajo contenedorTrabajo)
        {
            _registroRepository = registroRepository ?? throw new ArgumentNullException(nameof(registroRepository));
            _contenedorTrabajo = contenedorTrabajo ?? throw new ArgumentNullException(nameof(contenedorTrabajo));
        }

        // Acción para mostrar la lista de registros de alumnos
        public IActionResult Index()
        {
            var registros = _registroRepository.GetAllRegistros();
            return View(registros);
        }

        // Acción para mostrar el formulario de registro de alumno
        public IActionResult Create()
        {
            try
            {
                var materias = _contenedorTrabajo.Materia.GetAllMaterias();
                ViewBag.Materias = new SelectList(materias, "Id", "Nombre");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener las materias");
                return View();
            }
        }

        // Acción para procesar el formulario de registro de alumno
        [HttpPost]
        public IActionResult Create(Registro registro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _registroRepository.CreateRegistro(registro);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al crear el registro");
                    return View(registro);
                }
            }

            try
            {
                var materias = _contenedorTrabajo.Materia.GetAllMaterias();
                ViewBag.Materias = new SelectList(materias, "Id", "Nombre", registro.Materia);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al obtener las materias");
            }

            return View(registro);
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            
            try
            {
                var registro = _registroRepository.GetRegistro(id);
                if (registro == null)
                {
                    return NotFound();
                }

                var materias = _contenedorTrabajo.Materia.GetAllMaterias();
                ViewBag.Materias = new SelectList(materias, "Id", "Nombre", registro.Materia); // Asegúrate de establecer el valor seleccionado si es necesario

                return View(registro);
            }
            catch (Exception ex)
            {
                // Registrar la excepción
                // Log.Error(ex, "Error al obtener el registro para editar");
                ModelState.AddModelError("", "Error al obtener el registro para editar");
                return View();
            }
        }

        // Acción para procesar el formulario de edición de registro
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(Registro registro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _registroRepository.UpdateRegistro(registro);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Registrar la excepción
                    // Log.Error(ex, "Error al actualizar el registro");
                    ModelState.AddModelError("", "Error al actualizar el registro");
                    return View(registro);
                }
            }

            // Recargar las materias para el combo box
            try
            {
                var materias = _contenedorTrabajo.Materia.GetAllMaterias();
                ViewBag.Materias = new SelectList(materias, "Id", "Nombre", registro.Materia); // Asegúrate de establecer el valor seleccionado si es necesario
            }
            catch (Exception ex)
            {
                // Registrar la excepción
                // Log.Error(ex, "Error al obtener las materias en Edit (HttpPost)");
                ModelState.AddModelError("", "Error al obtener las materias");
            }

            return View(registro);
        }
        [Authorize(Roles = "Administrador")]/////////////
        // Acción para eliminar una registro
        [HttpPost]
        [Authorize(Roles = "Administrador")]/////////////
        public IActionResult Delete(int id)
        {
            try
            {
                _registroRepository.DeleteRegistro(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar el registro." });
            }
        }

        // Acción para buscar registros por CI
        [HttpGet]
        public IActionResult BuscarPorCI(string ci)
        {
            try
            {
                // Obtener registros por CI
                List<Registro> registros = _registroRepository.BuscarPorCI(ci);

                // Si hay registros, pasarlos a la vista Index para mostrarlos
                if (registros != null && registros.Any())
                {
                    return View("Index", registros);
                }
                else
                {
                    // Si no se encontraron registros, mostrar un mensaje en la vista Index
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
