namespace Alojat.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string NumCelular { get; set; } = string.Empty;

        public string DireccionDomicilio { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;   

        public string Password { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public int RolID { get; set; }
        public Rol? Rol { get; set; }
        public ICollection<Inmueble>? Inmuebles { get; set; }
    }
}
