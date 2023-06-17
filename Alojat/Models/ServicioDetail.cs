namespace Alojat.Models
{
    public class ServicioDetail
    {
        public int ServicioID { get; set; }
        public string ImagenServicio { get; set; }
        public bool EstadoAlquilerServicio { get; set; }
        public string TipoServicio { get; set; }
        public string UbicacionPiso { get; set; }
        public string DescripcionServicio { get; set; }
        public decimal Precio { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Direc { get; set; }
        public string Celular { get; set; }
        public int Punto { get; set; }
        public int Categoria { get; set; }
    }
}
