using Alojat.Data;
using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alojat.service
{
    public class SReferencia : IReferencia
    {
        private readonly AlquilerDbContext dbContext;

        public SReferencia(AlquilerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool ExistsPunto(int id)
        {
            return (dbContext.PuntoReferencia?.Any(e => e.PuntoReferenciaID == id)).GetValueOrDefault();
        }

        public PuntoReferencia FindPunto(int id)
        {
            return dbContext.PuntoReferencia.Find(id);
        }

        public PuntoReferencia FirstPunto(int id)
        {
            return dbContext.PuntoReferencia
                .FirstOrDefault(m => m.PuntoReferenciaID == id);
        }

        public List<PuntoReferencia> LisReferencia()
        {
            return dbContext.PuntoReferencia.ToList();
        }

        public List<PuntoReferencia>  ListReferen()
        {
            return dbContext.PuntoReferencia.ToList();
        }

        public void RemovePunto(PuntoReferencia referencia)
        {
            dbContext.PuntoReferencia.Remove(referencia);
            dbContext.SaveChanges();
        }

        public void SavePunto(PuntoReferencia referencia)
        {
            dbContext.PuntoReferencia.Add(referencia);
            dbContext.SaveChanges();
        }

        public void UpdatePunto(PuntoReferencia referencia)
        {
            dbContext.PuntoReferencia.Update(referencia);
            dbContext.SaveChanges();
        }
    }
}
