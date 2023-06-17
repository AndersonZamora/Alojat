using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alojat.Models
{
    public class Servicio
    {

        public int ServicioID { get; set; }
        public string ImagenServicio { get; set; } = "https://firebasestorage.googleapis.com/v0/b/lobos-marinos.appspot.com/o/Alojat%2Fajojat.jpg?alt=media&token=d66534de-af0c-4ee7-8a34-d5652b657ffa";
        public string UbicacionPiso { get; set; }
        public string DescripcionServicio { get; set; }
        public string TipoServicio { get; set; }
        public decimal Precio { get; set; }   
        public bool EstadoAlquilerServicio { get; set; }
        public int CategoriaID { get; set; }

        public Categoria? Categoria { get; set; }
        public int InmuebleID { get; set; }
        public Inmueble? Inmueble { get; set; }
    }
}
