using System.ComponentModel.DataAnnotations;

namespace Alojat.Models
{
    public class PuntoReferencia
    {
        public int PuntoReferenciaID { get; set; }
        public string NombrePuntoReferencia { get; set; } = "";
        public string DireccionReferencia { get; set; } = "";
        public string Latitud { get; set; } = "";
        public string Longitud { get; set; } = "";
        public ICollection<Inmueble>? Inmuebles { get; set; }
    }
}
