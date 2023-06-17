using Alojat.Models;

namespace Alojat.interfa
{
    public interface IServicio
    {
        List<Servicio> ListGetUser(int id);
        List<Servicio> ListGet();
        Servicio FirstOr(int id);
        void SaveServicio(Servicio servicio);
        void DeleteServicio(Servicio servicio);
        void UpdateServicio(Servicio servicio);
        bool ServicioExists(int id);
    }
}
