using Microsoft.EntityFrameworkCore;
using mvcProyectoAlmacen.Data.Repository;
using mvcProyectoAlmacen.Data.Repository.IRepository;
using mvcProyectoAlmacen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcProyectoAlmacen.Data.Repository
{
    public class ColegioRepository : Repository<Colegio>, IColegioRepository
    {
        private readonly ApplicationDbContext _db;
        public ColegioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Colegio Colegio)
        {
            var objDesdeDb = _db.Colegio.FirstOrDefault(s => s.Id == Colegio.Id);
            objDesdeDb.NombreColegio = Colegio.NombreColegio;
            objDesdeDb.Direccion = Colegio.Direccion;
        }

    }
}
