using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.EntityFrameworkCore;

namespace Alojat.service
{
    public class SCategoria : ICategoria
    {
        readonly AlquilerDbContext dbContext;

        public SCategoria(AlquilerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Categoria> LisCategoria()
        {
            return dbContext.Categoria.ToList();
        }

        public void SaveCategoria(Categoria categoria)
        {
            dbContext.Categoria.Add(categoria);
            dbContext.SaveChanges();
        }

        public bool ValidateCategoria(int id)
        {
            if (id == 0 || dbContext.Categoria == null)
            {
                return false;
            }

            return true;
        }

        public Categoria FindCategoria(int id)
        {
            return dbContext.Categoria.Find(id);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            dbContext.Categoria.Update(categoria);
            dbContext.SaveChanges();
        }

        public bool CategoriaExists(int id)
        {
            return (dbContext.Categoria?.Any(e => e.CategoriaID == id)).GetValueOrDefault();
        }

        public Categoria FirstCategoria(int id)
        {
            return dbContext.Categoria.FirstOrDefault(m => m.CategoriaID == id);
        }

        public void RemoveCategoria(Categoria categoria)
        {
            dbContext.Categoria.Remove(categoria);
            dbContext.SaveChangesAsync();
        }
    }
}
