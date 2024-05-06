using Microsoft.EntityFrameworkCore;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mvcProyectoAlmacen.Data.Repository
{
    public class RegistroRepository : Repository<Registro>, IRegistroRepository
    {
        private readonly ApplicationDbContext _db;

        public RegistroRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void CreateRegistro(Registro registro)
        {
            Add(registro);
            Save();
        }

        public void UpdateRegistro(Registro registro)
        {
            Update(registro);
            Save();
        }

        public void DeleteRegistro(int id)
        {
            var registro = Get(id);
            if (registro != null)
            {
                Remove(registro);
                Save();
            }
        }

        public Registro GetRegistro(int id)
        {
            return Get(id);
        }

        public IEnumerable<Registro> GetAllRegistros()
        {
            return GetAll();
        }

        public List<Registro> BuscarPorCI(string ci)
        {
            return _db.Registro.Where(r => r.CI == ci).ToList();
        }
    }
}


