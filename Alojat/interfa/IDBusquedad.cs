using Alojat.Models;
using System.Data;

namespace Alojat.interfa
{
    public interface IDBusquedad
    {
        Task<DataTable> DB_Servicio_Punto(Buscar buscar);
        Task<DataTable> DB_servicioByIdInmuble(int Id_Inm);
    }
}
