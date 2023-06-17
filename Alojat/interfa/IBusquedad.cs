using Alojat.Models;

namespace Alojat.interfa
{
    public interface IBusquedad
    {
        Task<IEnumerable<CatPun>> CatPun(Buscar buscar);
        Task<IEnumerable<ServicioDetail>> Servicios(int Id_Inm);
    }
}
