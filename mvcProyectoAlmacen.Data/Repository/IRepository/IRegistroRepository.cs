using mvcProyectoAlmacen.Models;
using System.Collections.Generic;

namespace mvcProyectoAlmacen.Data.Repository.IRepository
{
    public interface IRegistroRepository
    {
        void CreateRegistro(Registro registro);
        void UpdateRegistro(Registro registro);
        void DeleteRegistro(int id);
        Registro GetRegistro(int id);
        IEnumerable<Registro> GetAllRegistros();
        List<Registro> BuscarPorCI(string ci);
        //object GetRegistroById(int id);
        // Otros métodos del repositorio...
    }
}


