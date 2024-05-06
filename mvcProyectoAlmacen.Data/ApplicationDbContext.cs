using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mvcProyectoAlmacen.Models;

namespace mvcProyectoAlmacen.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Colegio> Colegio { get; set; }
        public DbSet<Registro> Registro { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Materia> Materia { get; set; }
    }
}
