using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mvcProyectoAlmacen.Data.Repository;
using mvcProyectoAlmacen.Models;

namespace mvcProyectoAlmacen.Data.Repository
{
    public class ContenedorTrabajo: IContenedorTrabajo
    {
        private readonly ApplicationDbContext _context;
        
        public ContenedorTrabajo(ApplicationDbContext context)
        {
            _context = context;
            //se agregan cada uno de los repositorios para que queden encapsulados
            Colegio = new ColegioRepository(_context);
            Usuario = new UsuarioRepository(_context);
            Materia = new MateriaRepository(_context);
            Registro = new RegistroRepository(_context);
        }


        public IColegioRepository Colegio { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public IRegistroRepository Registro { get; private set; }

        public IMateriaRepository Materia { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
