using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoAlmacen.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CI { get; set; }
        public string Materia { get; set; }
        public string Fecha { get; set; }
        public string HoraDeIngreso { get; set; }
        public string HoraDesalida { get; set; }
        public string Asistencia { get; set; }
    }
}
