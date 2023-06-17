using System.ComponentModel.DataAnnotations;

namespace Alojat.Models
{
    public class PuntoReferencia
    {
        [Key]
        [Display(Name = "PuntoReferencia ID")]
        public int PuntoReferenciaID { get; set; }
        [Display(Name = "Nombre del Punto")]
        [Required(ErrorMessage = "Nombre requerido.")]
        public string NombrePuntoReferencia { get; set; }
        [Display(Name = "Dirección de Referencia")]
        [Required(ErrorMessage = "Dirección requerida.")]
        public string DireccionReferencia { get; set; }
        [Display(Name = "Latitud Referencia")]
        [Required(ErrorMessage = "Latitud requerida")]
        public string Latitud { get; set; }
        [Display(Name = "Longitud Referencia")]
        [Required(ErrorMessage = "Longitud requerida")]
        public string Longitud { get; set; }
        public ICollection<Inmueble>? Inmuebles { get; set; }
    }
}
