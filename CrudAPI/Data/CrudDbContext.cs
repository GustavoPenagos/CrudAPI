using CrudAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudAPI.Data
{
    public class CrudDbContext: DbContext
    {
        public CrudDbContext(DbContextOptions<CrudDbContext>options):base(options)
        {
        }
        public DbSet<Alumnos> Alumnos { get; set; }
        public DbSet<Asignatura> Asignatura { get; set;}
        public DbSet<Profesor> Profesor { get; set;}
    }
}
