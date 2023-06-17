namespace Alojat.interfa
{
    public interface ISha
    {
        string ConvertirSha256(string texto);
        Task<string> SubirStorage(Stream archivo, string nombre);
    }
}
