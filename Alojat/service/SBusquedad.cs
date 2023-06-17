using Alojat.interfa;
using Alojat.Models;
using System.Data;

namespace Alojat.service
{
    public class SBusquedad : IBusquedad
    {
        private readonly IDBusquedad mBusquedad;

        public SBusquedad(IDBusquedad mBusquedad)
        {
            this.mBusquedad = mBusquedad;
        }

        public async Task<IEnumerable<CatPun>> CatPun(Buscar buscar)
        {
            DataTable data = await mBusquedad.DB_Servicio_Punto(buscar);

            List<CatPun> list = new();
             
            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    CatPun evento = new()
                    {
                        ServicioID = Convert.ToInt16(row["ServicioID"]),
                        InmuebleID = Convert.ToInt16(row["InmuebleID"]),
                        PuntoReferenciaID = Convert.ToInt16(row["PuntoReferenciaID"]),
                        DireccionInmueble = row["DireccionInmueble"].ToString(),
                        CategoriaID = Convert.ToInt16(row["CategoriaID"]),
                        UsuarioID = Convert.ToInt16(row["UsuarioID"]),
                        Nombres = row["Nombres"].ToString(),
                        Apellidos = row["Apellidos"].ToString(),
                        NombrePuntoReferencia = row["NombrePuntoReferencia"].ToString(),
                        ImagenInmueble = row["ImagenInmueble"].ToString(),
                        Precio = Convert.ToDecimal(row["Precio"])
                    };

                    list.Add(evento);
                }

                return list;
            }

            return list;
        }

        public async Task<IEnumerable<ServicioDetail>> Servicios(int Id_Inm)
        {
            DataTable data = await mBusquedad.DB_servicioByIdInmuble(Id_Inm);

            List<ServicioDetail> list = new();

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    ServicioDetail evento = new()
                    {
                        ServicioID = Convert.ToInt16(row["ServicioID"]),
                        ImagenServicio = row["ImagenServicio"].ToString(),
                        EstadoAlquilerServicio = (bool)row["EstadoAlquilerServicio"],
                        TipoServicio = row["TipoServicio"].ToString(),
                        UbicacionPiso = row["UbicacionPiso"].ToString(),
                        DescripcionServicio = row["DescripcionServicio"].ToString(),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        Latitud = row["Latitud"].ToString(),
                        Longitud = row["Longitud"].ToString(),
                        Direc = row["Direc"].ToString(),
                        Celular = row["Celular"].ToString(),
                        Punto = Convert.ToInt16(row["PuntoReferenciaID"]),
                        Categoria = Convert.ToInt16(row["CategoriaID"])
                    };

                    list.Add(evento);
                }

                return list;
            }

            return list;
        }
    }
}
