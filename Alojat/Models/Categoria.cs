using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alojat.Models
{
    public class Categoria
    {
       
        [Display(Name = "Categoría ID")]
        public int CategoriaID { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        [MinLength(3, ErrorMessage = "Error"),
        MaxLength(50, ErrorMessage = "Error")]
        [Required(ErrorMessage = "Nombre Requerido")]
        [Display(Name = "Nombre Categoría")]
        [RegularExpression("^[A-ZÑÁÉÍÓÚ, a-zñáéíóú, ^a-zA-Z0-9]+$", ErrorMessage = "Sólo se permiten letras.")]
        public string NombreCategoria { get; set; }
        public ICollection<Servicio>? Servicios { get; set; }
    }
}
