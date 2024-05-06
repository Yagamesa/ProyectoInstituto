using mvcProyectoAlmacen.Models;
using System.Collections.Generic;

namespace mvcProyectoAlmacen.Data.Repository.IRepository
{
    public interface IMateriaRepository
    {
        void CreateMateria(Materia materia);
        void UpdateMateria(Materia materia);
        void DeleteMateria(int id);
        Materia GetMateria(int id);
        IEnumerable<Materia> GetAllMaterias();
        List<Materia> BuscarPorID(string id);
        // Otros métodos específicos si los necesitas
    }
}


