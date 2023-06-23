using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVusuario
    {
        bool Validate(Usuario usuario, ModelStateDictionary modelState);
        bool UpdateCate(Usuario usuario, ModelStateDictionary modelState);
        bool UpdatePass(UserUpdate usuario, ModelStateDictionary modelState);
    }
}
