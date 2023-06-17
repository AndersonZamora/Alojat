using System.ComponentModel.DataAnnotations;

namespace Alojat.Models
{
    public class Inmueble
    {
        [Key]
        [Display(Name = "Inmueble ID")]
        public int InmuebleID { get; set; }

        public string ImagenInmueble { get; set; } = "https://firebasestorage.googleapis.com/v0/b/lobos-marinos.appspot.com/o/Alojat%2Fajojat.jpg?alt=media&token=d66534de-af0c-4ee7-8a34-d5652b657ffa";

        public string LatitudInmueble { get; set; }

        public string LongitudInmueble { get; set; }

        public string DireccionInmueble { get; set; }

        public string NumCelular { get; set; }

        public int PuntoReferenciaID { get; set; }

        public PuntoReferencia? PuntoReferencia { get; set; }
        public ICollection<Servicio>? Servicios { get; set; }
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
