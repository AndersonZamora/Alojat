using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.interfa
{
    public interface IVlogin
    {
        bool Validate(LoginModel login, ModelStateDictionary modelState);
        bool Register(RegistroUsuario registro, ModelStateDictionary modelState);
    }
}
