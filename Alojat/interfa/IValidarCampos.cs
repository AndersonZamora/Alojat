namespace Alojat.interfa
{
    public interface IValidarCampos
    {
        bool ValidarCaractes(string numString);
        bool ValidarnUMEROS(string numString);
        bool ValidarUnaPalabra(string numString);
        bool ValidarLetras(string numString);
        bool ValidarSoloLetras(string numString);
        bool ValidarLetras2(string numString);
        int ValidarEdad(string edad);
        bool ValidarEmail(string email);
        bool ValidarPrecio(string precio);
        bool Precio(string precio);
        bool ValidarNombreExpreciones(string nombre);
        bool ValidarStock(int nombre);
        bool ValidarDireccion(string direccion);
        bool ValidarCoordenadas(string coordenadas);
        bool ValidarLetrasNumeros(string piso);
        bool ValidarFecha(string fecha);
    }
}
