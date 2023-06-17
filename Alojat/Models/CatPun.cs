namespace Alojat.Models
{
    public class CatPun
    {
        public int ServicioID { get; set; }
        public int InmuebleID { get; set; }
        public int PuntoReferenciaID { get; set; }
        public string DireccionInmueble { get; set; }
        public int CategoriaID { get; set; }
        public int UsuarioID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombrePuntoReferencia { get; set; }
        public string ImagenInmueble { get; set; }
        public decimal Precio { get; set; }
    }
}
