using mvcProyectoAlmacen.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoAlmacen.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IColegioRepository Colegio { get; }
        IUsuarioRepository Usuario { get; }
        IMateriaRepository Materia { get; }
        IRegistroRepository Registro { get; }
        void Save();
    }
}
