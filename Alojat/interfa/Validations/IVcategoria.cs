using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVcategoria
    {
        bool Validate(Categoria categoria, ModelStateDictionary modelState);
        bool UpdateCate(Categoria categoria, ModelStateDictionary modelState);
    }
}
