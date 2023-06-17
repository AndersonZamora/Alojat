using Alojat.interfa;
using Alojat.Models;
using System.Data;
using System.Data.SqlClient;

namespace Alojat.service
{
    public class SDBusquedad : IDBusquedad
    {
        private readonly string con = "Server=.;Database=AlquilerDB;Trusted_Connection=True;MultipleActiveResultSets=True";

        public async Task<DataTable> DB_servicioByIdInmuble(int Id_Inm)
        {
            SqlConnection cn = new();
            DataTable data = new();

            try
            {
                cn.ConnectionString = con;
                SqlDataAdapter da = new("busqueda_servicioByIdInmuble", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_Inm", Id_Inm);

                await cn.OpenAsync();

                da.Fill(data);
                cn.Close();
                return data;

            }
            catch (Exception)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                return data;
            }
        }

        public async Task<DataTable> DB_Servicio_Punto(Buscar buscar)
        {
            SqlConnection cn = new();
            DataTable data = new();

            try
            {
                cn.ConnectionString = con;
                SqlDataAdapter da = new("busqueda_cat", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id_Cat", buscar.Tipo);
                da.SelectCommand.Parameters.AddWithValue("@Id_Ref", buscar.Referencia);

                await cn.OpenAsync();

                da.Fill(data);
                cn.Close();
                return data;

            }
            catch (Exception)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                return data;
            }
        }
    }
}
