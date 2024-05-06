using Microsoft.EntityFrameworkCore;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace mvcProyectoAlmacen.Data.Repository
{
    public class MateriaRepository : Repository<Materia>, IMateriaRepository
    {
        private readonly ApplicationDbContext _db;

        public MateriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void CreateMateria(Materia materia)
        {
            Add(materia);
            Save();
        }

        public void UpdateMateria(Materia materia)
        {
            Update(materia);
            Save();
        }

        public void DeleteMateria(int id)
        {
            var materia = Get(id);
            if (materia != null)
            {
                Remove(materia);
                Save();
            }
        }

        public Materia GetMateria(int id)
        {
            return Get(id);
        }

        public IEnumerable<Materia> GetAllMaterias()
        {
            return GetAll();
        }
        public List<Materia> BuscarPorID(string id)
        {
            int idInt;
            if (!int.TryParse(id, out idInt))
            {
                // Handle invalid input, like returning an empty list or throwing an exception
                return new List<Materia>();
            }

            return _db.Materia.Where(r => r.Id == idInt).ToList();
        }


        // Otros métodos específicos si los necesitas
    }
}

