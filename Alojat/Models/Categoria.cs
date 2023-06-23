namespace Alojat.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string NombreCategoria { get; set; } = "";
        public ICollection<Servicio>? Servicios { get; set; }
    }
}
