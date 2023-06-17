using System.ComponentModel.DataAnnotations;

namespace Alojat.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Usuario ID")]
        public int UsuarioID { get; set; }
        [Required(ErrorMessage = "Nombres requeridos.")]
        [RegularExpression("^[A-ZÑÁÉÍÓÚ, a-zñáéíóú, ^a-zA-Z0-9]+$", ErrorMessage = "Sólo se permiten letras.")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellidos requeridos.")]
        [RegularExpression("^[A-ZÑÁÉÍÓÚ, a-zñáéíóú, ^a-zA-Z0-9]+$", ErrorMessage = "Sólo se permiten letras.")]
        public string Apellidos { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Fecha requerida.")]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Número Celular")]
        [Required(ErrorMessage = "Número de celular requerido.")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Sólo se permiten números")]
        [MinLength(5, ErrorMessage = "El número debe tener mínimo 5 dígitos."), MaxLength(9, ErrorMessage = "El número debe tener como máximo 9 dígitos.")]
        public string NumCelular { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Dirección requerida.")]
        public string DireccionDomicilio { get; set; }
        [Required(ErrorMessage = "Email requerido.")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña requerida.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres"),
        MaxLength(20, ErrorMessage = "La contraseña no debe exceder en 30 caracteres")]
        public string? Password { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int RolID { get; set; }
        public Rol? Rol { get; set; }
        public ICollection<Inmueble>? Inmuebles { get; set; }
    }
}
