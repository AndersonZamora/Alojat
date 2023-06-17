using System.ComponentModel.DataAnnotations;

namespace Alojat.Models
{
    public class Rol
    {
        [Key]
        [Display(Name = "Rol ID")]
        public int RolID { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(20)]
        public string DescripcionRol { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
