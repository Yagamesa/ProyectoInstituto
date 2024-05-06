using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvcProyectoAlmacen.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Modulo { get; set; }
        public string Docente { get; set; }
        public string Turno { get; set; }
    }
}
