using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVpunto
    {
        bool Validate(PuntoReferencia punto, ModelStateDictionary modelState);
    }
}
