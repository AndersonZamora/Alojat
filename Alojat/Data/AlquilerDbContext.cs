using Alojat.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Alojat.Data
{
    public class AlquilerDbContext : IdentityDbContext
    {
        public AlquilerDbContext(DbContextOptions<AlquilerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public  virtual DbSet<PuntoReferencia> PuntoReferencia { get; set; }
        public virtual DbSet<Inmueble> Inmueble { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
    }
}
