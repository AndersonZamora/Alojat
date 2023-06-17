using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.EntityFrameworkCore;

namespace Alojat.service
{
    public class SServicio : IServicio
    {
        private readonly AlquilerDbContext dbContext;

        public SServicio(AlquilerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void DeleteServicio(Servicio servicio)
        {
            dbContext.Servicio.Remove(servicio);
            dbContext.SaveChanges();
        }

        public Servicio FirstOr(int id)
        {
            return dbContext.Servicio
                  .Include(s => s.Categoria)
                  .Include(i => i.Inmueble)
                  .FirstOrDefault(m => m.ServicioID == id);
        }

        public List<Servicio> ListGet()
        {
            return dbContext.Servicio.Include(i => i.Inmueble).ThenInclude(u => u.Usuario).Include(s => s.Categoria).ToList();
        }

        public List<Servicio> ListGetUser(int id)
        {
            return dbContext.Servicio.Include(i => i.Inmueble).ThenInclude(u => u.Usuario).Include(s => s.Categoria).Where(o => o.Inmueble.UsuarioID == id).ToList();
        }

        public void SaveServicio(Servicio servicio)
        {
            dbContext.Servicio.Add(servicio);
            dbContext.SaveChanges();
        }

        public bool ServicioExists(int id)
        {
            return (dbContext.Servicio?.Any(e => e.ServicioID == id)).GetValueOrDefault();
        }

        public void UpdateServicio(Servicio servicio)
        {
            dbContext.Servicio.Update(servicio);
            dbContext.SaveChanges();
        }
    }
}
