using Alojat.interfa;
using Alojat.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alojat.service
{
    public class SVpunto : IVpunto
    {
        ModelStateDictionary modelState;

        private readonly IValidarCampos validarCampos;

        public SVpunto(IValidarCampos validarCampos)
        {
            this.validarCampos = validarCampos;
        }

        public bool Validate(PuntoReferencia punto, ModelStateDictionary modelState)
        {
            this.modelState = modelState;

            if (!ValidarNombre(punto.NombrePuntoReferencia)) return false;
            if (!ValididarDireccion(punto.DireccionReferencia)) return false;
            if (!ValididarCoordenadas($"{punto.Latitud},{punto.Longitud}")) return false;

            return true;
        }

        bool ValididarDireccion(string direccionInmueble)
        {
            if (string.IsNullOrEmpty(direccionInmueble))
            {
                modelState.AddModelError("Direccion", "Este Campo es Obligatorio");
                return false;
            }

            if (!validarCampos.ValidarDireccion(direccionInmueble))
            {
                modelState.AddModelError("Direccion", "Ingrese una direccion correcta");
                return false;
            }

            return true;
        }

        bool ValidarNombre(string descrip)
        {
            if (string.IsNullOrEmpty(descrip))
            {
                modelState.AddModelError("Nombre", "Este campo es requerido");
                return false;
            }

            if (!validarCampos.ValidarLetrasNumeros(descrip))
            {
                modelState.AddModelError("Nombre", "Solo ingrese letras y numeros");
                return false;
            }

            return true;
        }

        bool ValididarCoordenadas(string coodenadas)
        {
            if (string.IsNullOrEmpty(coodenadas))
            {

                modelState.AddModelError("Coordenadas", "Coordenadas no validas");
                return false;
            };

            if (!validarCampos.ValidarCoordenadas(coodenadas))
            {
                modelState.AddModelError("Coordenadas", "Coordenadas no validas");
                return false;
            }

            return true;
        }
    }
}
