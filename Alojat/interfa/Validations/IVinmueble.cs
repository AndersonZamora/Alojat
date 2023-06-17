using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVinmueble
    {
        bool Validate(Inmueble inmueble, ModelStateDictionary modelState);
        void ValidateUpdate(Inmueble inmueble);
    }
}
