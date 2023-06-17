using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Alojat.service
{
    public class SInmueble : IInmueble
    {
        private readonly AlquilerDbContext dbContext;

        public SInmueble(AlquilerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Inmueble FindInmu(int id)
        {
            return dbContext.Inmueble.Find(id);
        }

        public List<Inmueble> ListInmueRefe()
        {
            return dbContext.Inmueble.Include(i => i.PuntoReferencia).Include(u => u.Usuario).ToList();
        }

        public List<Inmueble> ListInmueRefeUsu(int id)
        {
            return dbContext.Inmueble.Include(i => i.PuntoReferencia).Include(u => u.Usuario).Where(o => o.UsuarioID == id).ToList();
        }

        public void RemoveInmueble(Inmueble inmueble)
        {
            dbContext.Inmueble.Remove(inmueble);
            dbContext.SaveChangesAsync();
        }

        public void SaveInmueble(Inmueble inmueble)
        {
            dbContext.Inmueble.Add(inmueble);
            dbContext.SaveChanges();
        }

        public List<SelectListItem> SelectLis(int id)
        {
            return dbContext.Inmueble.Where(u => u.UsuarioID == id).Select(x => new SelectListItem
            {
                Text = x.DireccionInmueble,
                Value = x.InmuebleID.ToString()
            }).ToList();
        }

        public void UpdateInmueble(Inmueble inmueble)
        {
            dbContext.Inmueble.Update(inmueble);
            dbContext.SaveChanges();
        }
    }
}
