using Alojat.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alojat.interfa
{
    public interface ICategoria
    {
        void SaveCategoria(Categoria categoria);
        List<Categoria> LisCategoria();
        bool ValidateCategoria(int id);
        Categoria FindCategoria(int id);
        Categoria FirstCategoria(int id);
        void UpdateCategoria(Categoria categoria);
        bool CategoriaExists(int id);
        void RemoveCategoria(Categoria categoria);
    }
}
