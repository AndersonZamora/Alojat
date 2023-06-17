using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVservicio
    {
        bool Validate(Servicio servicio, ModelStateDictionary modelState);
        void ValidateUpdate(Servicio servicio);
    }
}
